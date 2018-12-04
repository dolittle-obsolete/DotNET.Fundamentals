/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Collections;
using Dolittle.Types;

namespace Dolittle.DependencyInversion.Booting
{
    /// <summary>
    /// Represents a <see cref="IContainer"/> used during booting
    /// </summary>
    internal class BootContainer : IContainer
    {
        IDictionary<Type, object>    _bindings;

        /// <summary>
        /// Initializes a new instance of <see cref="BootContainer"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for finding types</param>
        /// <param name="bindings"><see cref="IBindingCollection">Bindings</see> for the <see cref="BootContainer"/></param>
        public BootContainer(ITypeFinder typeFinder, IBindingCollection bindings)
        {
            _bindings = ConvertBindings(bindings);
            _bindings[typeof(IContainer)] = this;

            ProvideBootBindings(typeFinder);
        }

        /// <inheritdoc/>
        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        /// <summary>
        /// Gets the <see cref="IBindingCollection">bindings</see> discovered at boot
        /// </summary>
        public IBindingCollection   BootBindings { get; private set; }

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
                
                instances.Add(_bindings[parameter.ParameterType]);
            }

            var bindingProvider = constructor.Invoke(instances.ToArray());
            return bindingProvider;
        }

        void ProvideBootBindings(ITypeFinder typeFinder)
        {
            var bindingCollections = new List<IBindingCollection>();
            typeFinder
                .FindMultiple<ICanProvideBootBindings>()
                .ForEach(_ =>
                {
                    var instance = Get(_) as ICanProvideBootBindings;
                    var builder = new BindingProviderBuilder();
                    instance.Provide(builder);
                    var bindingCollection = builder.Build();
                    bindingCollections.Add(bindingCollection);

                    var bindings = ConvertBindings(bindingCollection);
                    bindings.ForEach(_bindings.Add);
                });
            BootBindings = new BindingCollection(bindingCollections.ToArray());
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
                        case Strategies.Type type: return type.Target;
                        case Strategies.TypeCallback typeCallback: return typeCallback.Target;
                    }

                    return null;
                }
            );            
        }
    }
}