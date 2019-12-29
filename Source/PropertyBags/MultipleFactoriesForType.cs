// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Exception that gets thrown when a type has more than one <see cref="ITypeFactory" /> to build it.
    /// </summary>
    public class MultipleFactoriesForType : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleFactoriesForType"/> class.
        /// </summary>
        /// <param name="message">A message describing the exception.</param>
        /// <param name="innerException">An inner exception that is the original source of the error.</param>
        public MultipleFactoriesForType(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}