/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Resilience
{
    /// <summary>
    /// Exception that gets thrown if there are multiple implementations of <see cref="IDefineNamedPolicy"/> in the system
    /// </summary>
    public class MultiplePolicyDefinersForNameFound : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MultiplePolicyDefinersForNameFound"/>
        /// </summary>
        public MultiplePolicyDefinersForNameFound(string name) : base($"Multiple implementations of IDefineNamedPolicy found for '{name}' - there can be only one") {}
    }
}