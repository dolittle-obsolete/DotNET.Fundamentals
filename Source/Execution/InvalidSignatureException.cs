// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;

namespace Dolittle.Execution
{
    /// <summary>
    /// Exception that is thrown when signature of a method does not match
    /// how it is called. Typically used when dynamically invoking a <see cref="WeakDelegate"/>.
    /// </summary>
    public class InvalidSignatureException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSignatureException"/> class.
        /// </summary>
        /// <param name="expectedSignature"><see cref="MethodInfo"/> that represents the expected signature.</param>
        public InvalidSignatureException(MethodInfo expectedSignature)
            : base($"Method '{expectedSignature.Name}' was invoked with the wrong signature, expected: {expectedSignature}")
        {
        }
    }
}
