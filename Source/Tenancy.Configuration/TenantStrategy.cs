// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.Tenancy.Configuration
{
    /// <summary>
    /// Represents the concept of a tenant strategy.
    /// </summary>
    public class TenantStrategy : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly convert from <see cref="string"/> to <see cref="TenantStrategy"/>.
        /// </summary>
        /// <param name="strategy">Strategy name.</param>
        public static implicit operator TenantStrategy(string strategy) => new TenantStrategy { Value = strategy };
    }
}
