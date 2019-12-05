// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Tenancy.Configuration
{
    /// <summary>
    /// The exception that gets thrown when the strategy configuration is of wrong <see cref="Type"/>.
    /// </summary>
    public class WrongStrategyConfiguration : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongStrategyConfiguration"/> class.
        /// </summary>
        /// <param name="expectedType">The expected <see cref="Type"/>.</param>
        public WrongStrategyConfiguration(Type expectedType)
            : base($"Expected a configuration of type {expectedType.FullName}")
        {
        }
    }
}
