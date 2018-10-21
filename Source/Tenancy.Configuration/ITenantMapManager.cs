/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Tenancy.Configuration
{
    /// <summary>
    /// Represents a system that manages the file that represents the tenancy mapping strategy
    /// </summary>
    public interface ITenantMapManager
    {
        /// <summary>
        /// Gets the <see cref="TenantStrategy"/> that is configured
        /// </summary>
        /// <value></value>
        TenantStrategy Strategy {get; }
        /// <summary>
        /// Gets the instance of a specific tenant mapping strategy
        /// </summary>
        /// <typeparam name="T">The strategy</typeparam>
        T InstanceOfStrategy<T>() where T : class;
        /// <summary>
        /// Gets the instance of a specific tenant mapping strategy
        /// </summary>
        /// <param name="strategyType"></param>
        /// <returns></returns>
        object InstanceOfStrategy(Type strategyType);
    }
}