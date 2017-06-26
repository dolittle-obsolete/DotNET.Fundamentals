/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using doLittle.Types;
using doLittle.Execution;

namespace doLittle.DependencyInversion.Conventions
{
    /// <summary>
    /// Represents a <see cref="IBindingConventionManager"/>
    /// </summary>
    [Singleton]
    public class BindingConventionManager : IBindingConventionManager
    {
        readonly IContainer _container;
        readonly ITypeFinder _typeFinder;
        readonly List<Type> _conventions;

        /// <summary>
        /// Initializes a new instance <see cref="BindingConventionManager"/>
        /// </summary>
        /// <param name="container">The <see cref="IContainer"/> that bindings are resolved to</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> to discover binding conventions with</param>
        public BindingConventionManager(IContainer container, ITypeFinder typeFinder)
        {
            _container = container;
            _typeFinder = typeFinder;
            _conventions = new List<Type>();
        }

        /// <inheritdoc/>
        public void Add(Type type)
        {
            if( !_conventions.Contains(type))
                _conventions.Add(type);
        }

        /// <inheritdoc/>
        public void Add<T>() where T : IBindingConvention
        {
            Add(typeof(T));
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            var boundServices = _container.GetBoundServices();
            var existingBindings = new Dictionary<Type, Type>();

            foreach (var boundService in boundServices)
                existingBindings[boundService] = boundService;

            var allTypes = _typeFinder.All;
            var services = allTypes.Where(t => !existingBindings.ContainsKey(t)).ToList();

            var resolvedServices = new List<Type>();

            foreach( var conventionType in _conventions )
            {
                var convention = _container.Get(conventionType) as IBindingConvention;
                if( convention != null )
                {
                    var servicesToResolve = services.Where(s => convention.CanResolve(_container, s) && !_container.HasBindingFor(s));

                    foreach (var service in servicesToResolve)
                    {
                        convention.Resolve(_container, service);
                        resolvedServices.Add(service);
                    }
                    resolvedServices.ForEach(t => services.Remove(t));
                }
            }
        }

        /// <inheritdoc/>
        public void DiscoverAndInitialize()
        {
            var conventionTypes = _typeFinder.FindMultiple<IBindingConvention>();
            foreach( var conventionType in conventionTypes )
                Add(conventionType);

            Initialize();
        }
    }
}