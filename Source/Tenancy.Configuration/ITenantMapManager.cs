// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Tenancy.Configuration
{
    /// <summary>
    /// Represents a system that manages the file that represents the tenancy mapping strategy.
    /// </summary>
    public interface ITenantMapManager
    {
        /// <summary>
        /// Gets the <see cref="TenantStrategy"/> that is configured.
        /// </summary>
        TenantStrategy Strategy { get; }

        /// <summary>
        /// Gets the instance of a specific tenant mapping strategy.
        /// </summary>
        /// <typeparam name="T">Type of strategy.</typeparam>
        /// <returns>Instance of a strategy.</returns>
        T InstanceOfStrategy<T>()
            where T : class;

        /// <summary>
        /// Gets the instance of a specific tenant mapping strategy.
        /// </summary>
        /// <param name="strategyType"><see cref="Type"/> of strategy.</param>
        /// <returns>Instance of a strategy.</returns>
        object InstanceOfStrategy(Type strategyType);
    }
}