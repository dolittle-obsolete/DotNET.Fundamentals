// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Execution;

namespace Dolittle.Services
{
    /// <summary>
    /// Exception that gets thrown when the <see cref="ExecutionContext"/> is not present on a header.
    /// </summary>
    public class ExecutionContextMissingInHeaderWhenAttemptingMethodInvocation : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContextMissingInHeaderWhenAttemptingMethodInvocation"/> class.
        /// </summary>
        /// <param name="method">Method that was attempted invoked.</param>
        public ExecutionContextMissingInHeaderWhenAttemptingMethodInvocation(string method)
            : base($"ExecutionContext is missing in the header when attempting to invocate '{method}'")
        {
        }
    }
}