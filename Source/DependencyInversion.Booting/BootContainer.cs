// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Booting;
using Dolittle.Collections;
using Dolittle.Logging;

namespace Dolittle.DependencyInversion.Booting
{
    /// <summary>
    /// Represents a <see cref="IContainer"/> used during booting.
    /// </summary>
    public class BootContainer : IContainer
    {
        readonly IDictionary<Type, IActivationStrategy> _bindings;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootContainer"/> class.
        /// </summary>
        /// <param name="bindings"><see cref="IBindingCollection">Bindings</see> for the <see cref="BootContainer"/>.</param>
        /// <param name="newBindingsNotifier"><see cref="ICanNotifyForNewBindings">Notifier</see> of new <see cref="Binding">bindings</see>.</param>
        public BootContainer(IBindingCollection bindings, ICanNotifyForNewBindings newBindingsNotifier)
        {
            _bindings = bindings.ToDictionary(_ => _.Service, _ => _.Strategy);

            _bindings[typeof(IContainer)] = new Strategies.Constant(this);
            _bindings[typeof(GetContainer)] = new Strategies.Constant((GetContainer)(() => this));

            newBindingsNotifier.SubscribeTo(_ => _.ToDictionary(_ => _.Service, _ => _.Strategy).ForEach(_bindings.Add));
        }

        /// <inheritdoc/>
        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        /// <inheritdoc/>
        public object Get(Type type)
        {
            if (_bindings.TryGetValue(type, out var strategy))
            {
                return strategy switch
                {
                    Strategies.Constant constant => constant.Target,
                    Strategies.Callback callback => callback.Target(),
                    Strategies.CallbackWithBindingContext callback => callback.Target(new BindingContext(type)),
                    Strategies.Type typeConstant => Create(typeConstant.Target),
                    Strategies.TypeCallback typeCallback => Create(typeCallback.Target()),
                    Strategies.TypeCallbackWithBindingContext typeCallback => Create(typeCallback.Target(new BindingContext(type))),
                    _ => null
                };
            }

            return Create(type);
        }

        object Create(Type type)
        {
            var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (constructors.Length == 0) return Activator.CreateInstance(type);
            if (constructors.Length > 1) throw new OnlySingleConstructorSupported(type);
            var constructor = constructors[0];

            var parameters = new List<object>();
            foreach (var parameter in constructor.GetParameters())
            {
                if (!_bindings.ContainsKey(parameter.ParameterType))
                    throw new ConstructorDependencyNotSupported(type, parameter.ParameterType, _bindings.Select(_ => _.Key));

                parameters.Add(Get(parameter.ParameterType));
            }

            return constructor.Invoke(parameters.ToArray());
        }
    }
}