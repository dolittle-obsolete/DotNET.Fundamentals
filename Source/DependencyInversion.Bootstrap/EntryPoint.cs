/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dolittle.Assemblies;
using Dolittle.DependencyInversion.Conventions;
using Dolittle.Reflection;
using Dolittle.Types;

namespace Dolittle.DependencyInversion.Bootstrap
{
    /// <summary>
    /// The entrypoint for DependencyInversion
    /// </summary>
    public class EntryPoint
    {
        /// <summary>
        /// Initialize the entire DependencyInversion pipeline
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> for the application</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for doing discovery</param>
        /// <returns><see cref="IBindingCollection"/>></returns>
        public static IBindingCollection DiscoverBindings(IAssemblies assemblies, ITypeFinder typeFinder)
        {
            var bindingConventionManager = new BindingConventionManager(typeFinder);
            var bindingsFromConventions = bindingConventionManager.DiscoverAndSetupBindings();
            var bindingsFromProviders = DiscoverBindingProvidersAndGetBindings(typeFinder);

            var bindingCollection = new BindingCollection(bindingsFromConventions, bindingsFromProviders);
            return bindingCollection;
        }

        /// <summary>
        /// Initialize the entire DependencyInversion pipeline
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> for the application</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for doing discovery</param>
        /// <param name="bindings">Additional bindings</param>
        /// <returns>Configured <see cref="IContainer"/></returns>
        public static IContainer Initialize(IAssemblies assemblies, ITypeFinder typeFinder, IEnumerable<Binding> bindings = null)
        {
            var discoveredBindings = DiscoverBindings(assemblies, typeFinder);

            IContainer container = null;

            var containerBindingBuilder = new BindingBuilder(Binding.For(typeof(IContainer)));
            containerBindingBuilder.To(()=> container).Singleton();
            var containerBinding = containerBindingBuilder.Build();

            var otherBindings = new List<Binding>();
            otherBindings.Add(containerBinding);
            if( bindings != null ) otherBindings.AddRange(bindings);

            var bindingCollection = new BindingCollection(discoveredBindings, otherBindings);
            container = DiscoverAndConfigureContainer(assemblies, typeFinder, bindingCollection);
            return container;
        }

        static IBindingCollection DiscoverBindingProvidersAndGetBindings(ITypeFinder typeFinder)
        {
            var bindingProviders = typeFinder.FindMultiple<ICanProvideBindings>();
            var bindingCollections = new ConcurrentBag<IBindingCollection>();

            Parallel.ForEach(bindingProviders, bindingProviderType =>
            {
                ThrowIfBindingProviderIsMissingDefaultConstructor(bindingProviderType);
                var bindingProvider = Activator.CreateInstance(bindingProviderType)as ICanProvideBindings;
                var bindingProviderBuilder = new BindingProviderBuilder();
                bindingProvider.Provide(bindingProviderBuilder);
                bindingCollections.Add(bindingProviderBuilder.Build());
            });

            var bindingCollection = new BindingCollection(bindingCollections.ToArray());
            return bindingCollection;
        }

        static IContainer DiscoverAndConfigureContainer(IAssemblies assemblies, ITypeFinder typeFinder, IBindingCollection bindingCollection)
        {
            var containerProviderType = typeFinder.FindSingle<ICanProvideContainer>();
            ThrowIfMissingContainerProvider(containerProviderType);
            var containerProvider = Activator.CreateInstance(containerProviderType)as ICanProvideContainer;

            var container = containerProvider.Provide(assemblies, bindingCollection);
            return container;
        }

        static void ThrowIfBindingProviderIsMissingDefaultConstructor(Type bindingProvider)
        {
            if (!bindingProvider.HasDefaultConstructor())throw new BindingProviderMustHaveADefaultConstructor(bindingProvider);
        }

        static void ThrowIfMissingContainerProvider(Type containerProvider)
        {
            if (containerProvider == null)throw new MissingContainerProvider();

        }
    }
}