// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Services
{
    /// <summary>
    /// Exception that gets thrown when the ping interval is not present in reverse call arguments context.
    /// </summary>
    public class ReverseCallArgumentsContextMissingPingInterval : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseCallArgumentsContextMissingPingInterval"/> class.
        /// </summary>
        public ReverseCallArgumentsContextMissingPingInterval()
            : base($"The reverse call arguments context is missing the ping interval")
        {
        }
    }
}
