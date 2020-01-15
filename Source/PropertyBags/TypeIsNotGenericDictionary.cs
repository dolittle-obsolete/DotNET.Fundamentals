// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="Type"/> is expected to implement <see cref="IDictionary{TKey, TValue}"/>.
    /// but it does not.
    /// </summary>
    public class TypeIsNotGenericDictionary : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeIsNotGenericDictionary"/> class.
        /// </summary>
        /// <param name="type"><see cref="Type"/> that is not a <see cref="IDictionary{TKey, TValue}"/>.</param>
        public TypeIsNotGenericDictionary(Type type)
            : base($"Type '{type.AssemblyQualifiedName}' does not implement IDictionary<,>.")
        {
        }
    }
}