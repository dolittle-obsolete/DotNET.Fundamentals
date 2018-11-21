/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.DependencyInversion;
using Dolittle.Lifecycle;
using Dolittle.Types;

namespace Dolittle.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IConfigurationObjectProviders"/>
    /// </summary>
    [Singleton]
    public class ConfigurationObjectProviders : IConfigurationObjectProviders
    {
        readonly ITypeFinder _typeFinder;
        readonly IContainer _container;

        readonly IEnumerable<ICanProvideConfigurationObjects>   _providers;

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationObjectProviders"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> to use for finding providers</param>
        /// <param name="container"><see cerf="IContainer"/> used to get instances</param>
        public ConfigurationObjectProviders(ITypeFinder typeFinder, IContainer container)
        {
            _typeFinder = typeFinder;
            _container = container;

            _providers = _typeFinder.FindMultiple<ICanProvideConfigurationObjects>()
                                    .Select(_ => _container.Get(_) as ICanProvideConfigurationObjects).ToArray();
        }

        /// <inheritdoc/>
        public object Provide(Type type)
        {
            var provider = GetProvidersFor(type).SingleOrDefault();
            if( provider == null ) throw new MissingProviderForConfigurationObject(type);
            return provider.Provide(type);
        }


        IEnumerable<ICanProvideConfigurationObjects>    GetProvidersFor(Type type)
        {
            var providers = _providers.Where(_ => _.CanProvide(type));
            ThrowIfMultipleProvidersCanProvide(type, providers);
            return providers;
        }

        void ThrowIfMultipleProvidersCanProvide(Type type, IEnumerable<ICanProvideConfigurationObjects> providers)
        {
            if (providers.Count() > 1) throw new MultipleProvidersProvidingConfigurationObject(type);
        }
    }
}
