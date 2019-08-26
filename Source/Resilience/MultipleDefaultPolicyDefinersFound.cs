/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Resilience
{
    /// <summary>
    /// Exception that gets thrown if there are multiple implementations of <see cref="ICanDefineDefaultPolicy"/> in the system
    /// </summary>
    public class MultipleDefaultPolicyDefinersFound : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MultipleDefaultPolicyDefinersFound"/>
        /// </summary>
        public MultipleDefaultPolicyDefinersFound() : base("Multiple implementations of ICanDefineDefaultPolicy found - there can be only one") {}
    }
}