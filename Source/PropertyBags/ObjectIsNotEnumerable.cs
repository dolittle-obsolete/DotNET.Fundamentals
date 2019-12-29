// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Exception that gets thrown when an object is not enumerable.
    /// </summary>
    public class ObjectIsNotEnumerable : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectIsNotEnumerable"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public ObjectIsNotEnumerable(string message)
            : base(message)
        {
        }
    }
}