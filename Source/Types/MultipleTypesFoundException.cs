// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Dolittle.Types
{
    /// <summary>
    /// The exception that is thrown when multiple types are found and not allowed.
    /// </summary>
    public class MultipleTypesFoundException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleTypesFoundException"/> class.
        /// </summary>
        /// <param name="type">Type that multiple of it.</param>
        /// <param name="typesFound">The types that was found.</param>
        public MultipleTypesFoundException(Type type, IEnumerable<Type> typesFound)
            : base($"More than one type found for '{type.FullName}' - types found : [{string.Join(",", typesFound.Select(_ => _.FullName))}]")
        {
        }
    }
}