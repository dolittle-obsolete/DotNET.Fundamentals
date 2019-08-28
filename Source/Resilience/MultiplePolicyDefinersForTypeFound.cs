/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Resilience
{
    /// <summary>
    /// Exception that gets thrown if there are multiple implementations of <see cref="IDefinePolicyForType"/> in the system
    /// </summary>
    public class MultiplePolicyDefinersForTypeFound : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MultiplePolicyDefinersForTypeFound"/>
        /// </summary>
        public MultiplePolicyDefinersForTypeFound(Type type) : base($"Multiple implementations of IDefinePolicyForType found for '{type.AssemblyQualifiedName}' - there can be only one") {}
    }
}