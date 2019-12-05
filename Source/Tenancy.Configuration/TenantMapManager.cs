// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Lifecycle;

namespace Dolittle.Tenancy.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="ITenantMapManager"/>.
    /// </summary>
    [Singleton]
    public class TenantMapManager : ITenantMapManager
    {
        readonly ITenantStrategyLoader _tenantStrategyLoader;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantMapManager"/> class.
        /// </summary>
        /// <param name="tenantStrategyLoader"><see cref="ITenantStrategyLoader"/> for loading strategies.</param>
        public TenantMapManager(ITenantStrategyLoader tenantStrategyLoader)
        {
            _tenantStrategyLoader = tenantStrategyLoader;
        }

        /// <inheritdoc/>
        public TenantStrategy Strategy => _tenantStrategyLoader.Strategy;

        /// <inheritdoc/>
        public T InstanceOfStrategy<T>()
            where T : class => (T)InstanceOfStrategy(typeof(T));

        /// <inheritdoc/>
        public object InstanceOfStrategy(Type strategyType)
        {
            var instance = _tenantStrategyLoader.GetStrategyInstance(strategyType);
            if (instance == null || !strategyType.IsInstanceOfType(instance)) throw new WrongStrategyConfiguration(strategyType);

            return instance;
        }
    }
}