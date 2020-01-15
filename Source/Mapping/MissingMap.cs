// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Mapping
{
    /// <summary>
    /// Exception that gets thrown when one asks for a map for unknown combination of source and target.
    /// </summary>
    public class MissingMap : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingMap"/> class.
        /// </summary>
        /// <param name="source"><see cref="Type">Source type</see>.</param>
        /// <param name="target"><see cref="Type">Target type</see>.</param>
        public MissingMap(Type source, Type target)
            : base($"Missing map for given combination of '{source.FullName}' (SOURCE) and '{target.FullName}' (TARGET)")
        {
        }
    }
}
