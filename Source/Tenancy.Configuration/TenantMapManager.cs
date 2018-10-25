using System;
using Dolittle.Lifecycle;

namespace Dolittle.Tenancy.Configuration
{
    /// <inheritdoc/>
    [Singleton]
    public class TenantMapManager : ITenantMapManager
    {
        readonly ITenantStrategyLoader _tenantStrategyLoader;

        /// <summary>
        /// Instantiates an instance of <see cref="TenantMapManager"/>
        /// </summary>
        /// <param name="tenantStrategyLoader"></param>
        public TenantMapManager(ITenantStrategyLoader tenantStrategyLoader)
        {
            _tenantStrategyLoader = tenantStrategyLoader;
        }

        /// <inheritdoc/>
        public TenantStrategy Strategy => _tenantStrategyLoader.Strategy;

        /// <inheritdoc/>
        public T InstanceOfStrategy<T>() where T : class => (T)InstanceOfStrategy(typeof(T));
        
        /// <inheritdoc/>
        public object InstanceOfStrategy(Type strategyType)
        {
            var instance = _tenantStrategyLoader.GetStrategyInstance(strategyType);
            if (instance == null || !strategyType.IsInstanceOfType(instance)) throw new WrongStrategyConfiguration(strategyType);

            return instance;
        }
    }
}