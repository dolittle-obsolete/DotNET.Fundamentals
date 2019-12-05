// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Tenancy.Configuration
{
    /// <summary>
    /// Represents a representation of a strategy for retrieving the tenancy information.
    /// </summary>
    public interface IRepresentATenantStrategy
    {
        /// <summary>
        /// Gets the <see cref="TenantStrategy"/> that this representation represents.
        /// </summary>
        TenantStrategy Strategy { get; }

        /// <summary>
        /// Gets the <see cref="Type"/> of the configuration of the <see cref="TenantStrategy"/>.
        /// </summary>
        Type StrategyConfigurationType { get; }
    }
}