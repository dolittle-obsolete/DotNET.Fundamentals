// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.DependencyInversion;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Types;

namespace Dolittle.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IConfigurationObjectProviders"/>.
    /// </summary>
    [Singleton]
    public class ConfigurationObjectProviders : IConfigurationObjectProviders
    {
        readonly ITypeFinder _typeFinder;
        readonly IContainer _container;
        readonly ILogger _logger;
        readonly IEnumerable<ICanProvideConfigurationObjects> _providers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationObjectProviders"/> class.
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> to use for finding providers.</param>
        /// <param name="container"><see cerf="IContainer"/> used to get instances.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public ConfigurationObjectProviders(
            ITypeFinder typeFinder,
            IContainer container,
            ILogger logger)
        {
            _typeFinder = typeFinder;
            _container = container;
            _logger = logger;

            _providers = _typeFinder.FindMultiple<ICanProvideConfigurationObjects>()
                .Select(_ =>
                {
                    _logger.Trace($"Configuration Object provider : {_.AssemblyQualifiedName}");
                    return _container.Get(_) as ICanProvideConfigurationObjects;
                }).ToArray();
        }

        /// <inheritdoc/>
        public object Provide(Type type)
        {
            _logger.Trace($"Try to provide '{type.GetFriendlyConfigurationName()} - {type.AssemblyQualifiedName}'");
            var provider = GetProvidersFor(type).SingleOrDefault();
            if (provider == null)
            {
                if (HasDefaultConfigurationProviderFor(type)) return ProvideDefaultConfigurationFor(type);
                throw new MissingProviderForConfigurationObject(type);
            }

            _logger.Trace($"Provide '{type.GetFriendlyConfigurationName()} - {type.AssemblyQualifiedName}' using {provider.GetType().AssemblyQualifiedName}");
            return provider.Provide(type);
        }

        bool HasDefaultConfigurationProviderFor(Type type)
        {
            var providerType = typeof(ICanProvideDefaultConfigurationFor<>).MakeGenericType(type);
            var actualTypes = _typeFinder.FindMultiple(providerType);
            ThrowIfMultipleDefaultProvidersFound(type, actualTypes);
            return actualTypes.Count() == 1;
        }

        object ProvideDefaultConfigurationFor(Type type)
        {
            var providerType = typeof(ICanProvideDefaultConfigurationFor<>).MakeGenericType(type);
            var actualType = _typeFinder.FindSingle(providerType);
            var instance = _container.Get(actualType);
            var method = instance.GetType().GetMethod("Provide", BindingFlags.Public | BindingFlags.Instance);
            return method.Invoke(instance, null);
        }

        IEnumerable<ICanProvideConfigurationObjects> GetProvidersFor(Type type)
        {
            var providers = _providers.Where(_ =>
            {
                _logger.Trace((string)$"Ask '{_.GetType().AssemblyQualifiedName}' if it can provide the configuration type '{type.GetFriendlyConfigurationName()} - {type.AssemblyQualifiedName}'");
                return _.CanProvide(type);
            });
            ThrowIfMultipleProvidersCanProvide(type, providers);
            return providers;
        }

        void ThrowIfMultipleDefaultProvidersFound(Type type, IEnumerable<Type> actualTypes)
        {
            if (actualTypes.Count() > 1) throw new MultipleDefaultConfigurationProvidersFoundForConfigurationObject(type);
        }

        void ThrowIfMultipleProvidersCanProvide(Type type, IEnumerable<ICanProvideConfigurationObjects> providers)
        {
            if (providers.Count() > 1) throw new MultipleProvidersProvidingConfigurationObject(type);
        }
    }
}