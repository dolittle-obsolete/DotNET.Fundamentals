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
using Dolittle.Scheduling;
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
        /// <param name="scheduler"><see cref="IScheduler"/> for scheduling work</param>
        /// <param name="logger"><see cref="ILogger"/> for doing logging</param>
        /// <returns><see cref="IBindingCollection"/>></returns>
        public static IBindingCollection DiscoverBindings(IAssemblies assemblies, ITypeFinder typeFinder, IScheduler scheduler, ILogger logger)
        {
            logger.Information("Discover Bindings");
            var bindingConventionManager = new BindingConventionManager(typeFinder, scheduler, logger);

            logger.Information("Discover and setup bindings");
            var bindingsFromConventions = bindingConventionManager.DiscoverAndSetupBindings();

            logger.Information("Discover binding providers and get bindings");
            var bindingsFromProviders = DiscoverBindingProvidersAndGetBindings(typeFinder, scheduler);

            logger.Information("Compose bindings in new collection");
            var bindingCollection = new BindingCollection(bindingsFromConventions, bindingsFromProviders);

            foreach( var binding in bindingCollection )
            {
                logger.Information($"Discovered Binding : {binding.Service.AssemblyQualifiedName} - {binding.Strategy.GetType().Name}");
            }

            return bindingCollection;
        }

        /// <summary>
        /// Initialize the entire DependencyInversion pipeline
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> for the application</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for doing discovery</param>
        /// <param name="scheduler"><see cref="IScheduler"/> for scheduling work</param>
        /// <param name="logger"><see cref="ILogger"/> for doing logging</param>
        /// <param name="bindings">Additional bindings</param>
        /// <returns>Configured <see cref="IContainer"/> and <see cref="IBindingCollection"/></returns>
        public static BootResult Start(IAssemblies assemblies, ITypeFinder typeFinder, IScheduler scheduler, ILogger logger, IEnumerable<Binding> bindings = null)
        {
            logger.Information("DependencyInversion start");

            IContainer container = null;
            var otherBindings = new List<Binding>();
            if( bindings != null ) otherBindings.AddRange(bindings);
            otherBindings.Add(Bind(typeof(IContainer), () => container, true));

            logger.Information("Build bindings");
            var bindingCollection = BuildBindings(assemblies, typeFinder, scheduler, logger, otherBindings);

            logger.Information("Discover container");
            container = DiscoverAndConfigureContainer(assemblies, typeFinder, bindingCollection);

            logger.Information("Return boot result");
            return new BootResult(container, bindingCollection);
        }


        /// <summary>
        /// Initialize the entire DependencyInversion pipeline with a specified <see cref="Type"/> of container
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> for the application</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for doing discovery</param>
        /// <param name="logger"><see cref="ILogger"/> for doing logging</param>
        /// <param name="scheduler"><see cref="IScheduler"/> for scheduling work</param>
        /// <param name="containerType"><see cref="Type"/>Container type</param>
        /// <param name="bindings">Additional bindings</param>
        /// <returns>Configured <see cref="IContainer"/> and <see cref="IBindingCollection"/></returns>
        public static IBindingCollection Start(IAssemblies assemblies, ITypeFinder typeFinder, IScheduler scheduler, ILogger logger, Type containerType, IEnumerable<Binding> bindings = null)
        {
            var otherBindings = new List<Binding>();
            if( bindings != null ) otherBindings.AddRange(bindings);
            otherBindings.Add(Bind(typeof(IContainer), containerType, true));
            var bindingCollection = BuildBindings(assemblies, typeFinder, scheduler, logger, otherBindings);
            return bindingCollection;
        }

        static IBindingCollection BuildBindings(IAssemblies assemblies, ITypeFinder typeFinder, IScheduler scheduler, ILogger logger, IEnumerable<Binding> bindings)
        {
            logger.Information("Discover bindings");
            var discoveredBindings = DiscoverBindings(assemblies, typeFinder, scheduler, logger);

            var otherBindings = new List<Binding>();
            
            logger.Information("Add other bindings");
            otherBindings.Add(Bind(typeof(IAssemblies), assemblies));
            otherBindings.Add(Bind(typeof(ITypeFinder), typeFinder));
            otherBindings.Add(Bind(typeof(ILogger), logger));

            logger.Information("Add bindings found");
            if (bindings != null) otherBindings.AddRange(bindings);

            logger.Information("Create a new binding collection");

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
        

        static IBindingCollection DiscoverBindingProvidersAndGetBindings(ITypeFinder typeFinder, IScheduler scheduler)
        {
            var bindingProviders = typeFinder.FindMultiple<ICanProvideBindings>();
            var bindingCollections = new ConcurrentBag<IBindingCollection>();

            scheduler.PerformForEach(bindingProviders, bindingProviderType =>
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