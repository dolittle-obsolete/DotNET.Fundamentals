/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Booting;
using Dolittle.Collections;
using Dolittle.Types;

namespace Dolittle.DependencyInversion.Booting
{
    /// <summary>
    /// Represents a <see cref="IContainer"/> used during booting
    /// </summary>
    public class BootContainer : IContainer
    {
        IDictionary<Type, object>    _bindings;

        /// <summary>
        /// Initializes a new instance of <see cref="BootContainer"/>
        /// </summary>
        /// <param name="bindings"><see cref="IBindingCollection">Bindings</see> for the <see cref="BootContainer"/></param>
        /// <param name="newBindingsNotifier"><see cref="ICanNotifyForNewBindings">Notifier</see> of new <see cref="Binding">bindings</see></param>
        public BootContainer(IBindingCollection bindings, ICanNotifyForNewBindings newBindingsNotifier)
        {
            _bindings = ConvertBindings(bindings);
            _bindings[typeof(IContainer)] = this;

            newBindingsNotifier.SubscribeTo(_ => ConvertBindings(_).ForEach(_bindings.Add));
        }

        /// <inheritdoc/>
        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        /// <inheritdoc/>
        public object Get(Type type)
        {
            var constructors = type.GetConstructors(BindingFlags.Public|BindingFlags.Instance);
            if( constructors.Length == 0 ) return Activator.CreateInstance(type) as ICanProvideBindings;
            if( constructors.Length > 1 ) throw new OnlySingleConstructorSupported(type);
            
            var instances = new List<object>();

            var constructor = constructors[0];
            var parameters = constructor.GetParameters();

            foreach( var parameter in parameters )
            {
                if( !_bindings.ContainsKey(parameter.ParameterType)) 
                    throw new ConstructorDependencyNotSupported(type, parameter.ParameterType, _bindings.Select(_ => _.Key));

                var binding = _bindings[parameter.ParameterType];
                if( binding is Delegate && parameter.ParameterType != typeof(GetContainer) )
                {
                    var bindingDelegate = binding as Delegate;
                    binding = bindingDelegate.DynamicInvoke();
                }
                
                instances.Add(binding);
            }

            var bindingProvider = constructor.Invoke(instances.ToArray());
            return bindingProvider;
        }


        IDictionary<Type, object> ConvertBindings(IBindingCollection bindings)
        {
            return bindings.ToDictionary(
                _ => _.Service,
                _ => {
                    switch( _.Strategy )
                    {
                        case Strategies.Constant constant: return constant.Target;
                        case Strategies.Callback callback: return callback.Target;
                        case Strategies.CallbackWithBindingContext callback: return callback.Target;
                        case Strategies.Type type: return type.Target;
                        case Strategies.TypeCallback typeCallback: return typeCallback.Target;
                        case Strategies.TypeCallbackWithBindingContext typeCallback: return typeCallback.Target;
                    }

                    return null;
                }
            );            
        }
    }
}