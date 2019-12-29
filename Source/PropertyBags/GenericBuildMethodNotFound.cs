// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Exception that gets thrown when getting the generic Build method of an <see cref="IObjectFactory"/>.
    /// </summary>
    public class GenericBuildMethodNotFound : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericBuildMethodNotFound"/> class.
        /// </summary>
        /// <param name="type">The type where Build method is not found.</param>
        public GenericBuildMethodNotFound(Type type)
            : base($"Generic method taking one generic argument called Build was not found in the {type.AssemblyQualifiedName}")
        {
        }
    }
}