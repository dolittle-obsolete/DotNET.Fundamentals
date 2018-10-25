/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Dolittle.Tenancy.Configuration
{
    /// <summary>
    /// Represents a system that loads the tenancy strategy configuration
    /// </summary>
    public interface ITenantStrategyLoader
    {

        /// <summary>
        /// Gets the loaded <see cref="TenantStrategy"/>
        /// </summary>
        TenantStrategy Strategy {get; }
        /// <summary>
        /// Gets the actual configuration for the <see cref="TenantStrategy"/>
        /// </summary>
        /// <param name="strategyType"></param>
        object GetStrategyInstance(Type strategyType);
    }
}