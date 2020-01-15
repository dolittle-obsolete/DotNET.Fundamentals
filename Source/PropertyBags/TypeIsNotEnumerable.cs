// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Exception that gets thrown when a type was expected to be an enumerable, but wasn't.
    /// </summary>
    public class TypeIsNotEnumerable : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeIsNotEnumerable"/> class.
        /// </summary>
        /// <param name="type"><see cref="Type"/> that was expected to be an enumerable.</param>
        public TypeIsNotEnumerable(Type type)
            : base($"The type {type.FullName} was expected to be an Enumerable, but wasn't")
        {
        }
    }
}