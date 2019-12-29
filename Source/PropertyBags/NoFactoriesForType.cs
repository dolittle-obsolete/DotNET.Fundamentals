// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Exception that gets thrown when a type does not have a factory capable of constructing it.
    /// </summary>
    public class NoFactoriesForType : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoFactoriesForType"/> class.
        /// </summary>
        /// <param name="message">A message describing the exception.</param>
        public NoFactoriesForType(string message)
            : base(message)
        {
        }
    }
}