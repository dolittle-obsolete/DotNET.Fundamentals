/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using doLittle.Execution;
using doLittle.Reflection;
using doLittle.Types;

namespace doLittle.DependencyInversion.Conventions
{
    /// <summary>
    /// Represents a <see cref="IBindingConventionManager"/>
    /// </summary>
    [Singleton]
    public class BindingConventionManager : IBindingConventionManager
    {
        readonly ITypeFinder _typeFinder;
        readonly List<Type> _conventions;

        /// <summary>
        /// Initializes a new instance <see cref="BindingConventionManager"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> to discover binding conventions with</param>
        public BindingConventionManager(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
            _conventions = new List<Type>();
        }

        /// <inheritdoc/>
        public IBindingCollection DiscoverAndSetupBindings()
        {
            var bindingCollections = new ConcurrentBag<IBindingCollection>();

            var allTypes = _typeFinder.All;

            var conventionTypes = _typeFinder.FindMultiple<IBindingConvention>();
            Parallel.ForEach(conventionTypes, conventionType =>
            {
                ThrowIfBindingConventionIsMissingDefaultConstructor(conventionType);
                var convention = Activator.CreateInstance(conventionType)as IBindingConvention;
                var servicesToResolve = allTypes.Where(service => convention.CanResolve(service));

                var bindings = new ConcurrentBag<Binding>();
                Parallel.ForEach(servicesToResolve, service =>
                {
                    var bindingBuilder = new BindingBuilder(Binding.For(service));
                    convention.Resolve(service, bindingBuilder);
                    bindings.Add(bindingBuilder.Build());
                });

                var bindingCollection = new BindingCollection(bindings);
                bindingCollections.Add(bindingCollection);
            });

            var aggregatedBindingCollection = new BindingCollection(bindingCollections.ToArray());
            return aggregatedBindingCollection;
        }

        static void ThrowIfBindingConventionIsMissingDefaultConstructor(Type bindingProvider)
        {
            if (!bindingProvider.HasDefaultConstructor())throw new BindingConventionMustHaveADefaultConstructor(bindingProvider);
        }
    }
}