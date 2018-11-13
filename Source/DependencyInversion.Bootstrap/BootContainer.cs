/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Assemblies;
using Dolittle.Logging;
using Dolittle.Scheduling;
using Dolittle.Types;

namespace Dolittle.DependencyInversion.Bootstrap
{
    /// <summary>
    /// Represents a <see cref="IContainer"/> used during booting
    /// </summary>
    internal class BootContainer : IContainer
    {
        readonly Dictionary<Type, object>    _bindings;

        /// <summary>
        /// Initializes a new instance of <see cref="BootContainer"/>
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> with all assemblies available</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for finding available types</param>
        /// <param name="scheduler"><see cref="IScheduler"/> for scheduling work</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        /// <param name="getContainer"><see cref="GetContainer"/> for getting container</param>
        public BootContainer(
            IAssemblies assemblies,
            ITypeFinder typeFinder,
            IScheduler scheduler,
            ILogger logger,
            GetContainer getContainer)
        {
            _bindings = new Dictionary<Type, object> {
                { typeof(IAssemblies), assemblies },
                { typeof(ITypeFinder), typeFinder },
                { typeof(IScheduler), scheduler },
                { typeof(ILogger), logger },
                { typeof(GetContainer), getContainer }
            };
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
                
                instances.Add(_bindings[parameter.ParameterType]);
            }

            var bindingProvider = Activator.CreateInstance(type, instances.ToArray());
            return bindingProvider;
        }
    }
}