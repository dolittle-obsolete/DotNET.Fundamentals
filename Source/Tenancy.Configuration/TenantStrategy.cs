/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Dolittle.Tenancy.Configuration
{
    /// <summary>
    /// Represents the concept of a tenant strategy
    /// </summary>
    public class TenantStrategy : ConceptAs<string>
    {

        /// <summary>
        /// Implicitly convert from <see cref="string"/> to <see cref="TenantStrategy"/>
        /// </summary>
        /// <param name="strategy"></param>
        public static implicit operator TenantStrategy(string strategy)
        {
            return new TenantStrategy { Value = strategy };
        }
    }
}
