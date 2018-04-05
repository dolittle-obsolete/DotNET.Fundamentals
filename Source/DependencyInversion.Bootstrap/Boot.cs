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
using Dolittle.Logging;
using Dolittle.Reflection;
using Dolittle.Types;

namespace Dolittle.DependencyInversion.Bootstrap
{

    /// <summary>
    /// The entrypoint for DependencyInversion
    /// </summary>
    public class Boot
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
        /// <param name="logger"><see cref="ILogger"/> for doing logging</param>
        /// <param name="bindings">Additional bindings</param>
        /// <returns>Configured <see cref="IContainer"/> and <see cref="IBindingCollection"/></returns>
        public static BootResult Start(IAssemblies assemblies, ITypeFinder typeFinder, ILogger logger, IEnumerable<Binding> bindings = null)
        {
            IContainer container = null;
            var otherBindings = new List<Binding>();
            if( bindings != null ) otherBindings.AddRange(bindings);
            otherBindings.Add(Bind(typeof(IContainer), () => container, true));
            var bindingCollection = BuildBindings(assemblies, typeFinder, logger, otherBindings);

            container = DiscoverAndConfigureContainer(assemblies, typeFinder, bindingCollection);
            return new BootResult(container, bindingCollection);
        }


        /// <summary>
        /// Initialize the entire DependencyInversion pipeline with a specified <see cref="Type"/> of container
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> for the application</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for doing discovery</param>
        /// <param name="logger"><see cref="ILogger"/> for doing logging</param>
        /// <param name="containerType"><see cref="Type"/>Container type</param>
        /// <param name="bindings">Additional bindings</param>
        /// <returns>Configured <see cref="IContainer"/> and <see cref="IBindingCollection"/></returns>
        public static IBindingCollection Start(IAssemblies assemblies, ITypeFinder typeFinder, ILogger logger, Type containerType, IEnumerable<Binding> bindings = null)
        {
            var otherBindings = new List<Binding>();
            if( bindings != null ) otherBindings.AddRange(bindings);
            otherBindings.Add(Bind(typeof(IContainer), containerType, true));
            var bindingCollection = BuildBindings(assemblies, typeFinder, logger, otherBindings);
            return bindingCollection;
        }

        static IBindingCollection BuildBindings(IAssemblies assemblies, ITypeFinder typeFinder, ILogger logger, IEnumerable<Binding> bindings)
        {
            var discoveredBindings = DiscoverBindings(assemblies, typeFinder);

            var otherBindings = new List<Binding>();
            
            otherBindings.Add(Bind(typeof(IAssemblies), assemblies));
            otherBindings.Add(Bind(typeof(ITypeFinder), typeFinder));
            otherBindings.Add(Bind(typeof(ILogger), logger));

            if (bindings != null) otherBindings.AddRange(bindings);

            var bindingCollection = new BindingCollection(discoveredBindings, otherBindings);
            return bindingCollection;
        }

        static Binding Bind(Type type, Type target, bool singleton = false)
        {
            var containerBindingBuilder = new BindingBuilder(Binding.For(type));
            var scope = containerBindingBuilder.To(target);
            if( singleton ) scope.Singleton();
            return containerBindingBuilder.Build();
        }

        static Binding Bind(Type type, object target)
        {
            var containerBindingBuilder = new BindingBuilder(Binding.For(type));
            var scope = containerBindingBuilder.To(target);
            scope.Singleton();
            return containerBindingBuilder.Build();
        }

        static Binding Bind(Type type, Func<object> target, bool singleton = false)
        {
            var containerBindingBuilder = new BindingBuilder(Binding.For(type));
            var scope = containerBindingBuilder.To(target);
            if( singleton ) scope.Singleton();
            return containerBindingBuilder.Build();
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