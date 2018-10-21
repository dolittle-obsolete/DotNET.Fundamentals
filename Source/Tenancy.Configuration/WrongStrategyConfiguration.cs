/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Tenancy.Configuration
{
    /// <summary>
    /// The exception that gets thrown when the strategy configuration is of wrong <see cref="Type"/>
    /// </summary>
    public class WrongStrategyConfiguration : Exception
    {
        /// <summary>
        /// Instantiates an instance of <see cref="WrongStrategyConfiguration"/>
        /// </summary>
        /// <param name="expectedType"></param>
        public WrongStrategyConfiguration(Type expectedType)
            : base($"Expected a configuration of type {expectedType.FullName}")
        {}
    }
}