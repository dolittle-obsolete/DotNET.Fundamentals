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
        /// <param name="type">Type with multiple factories.</param>
        public MultipleFactoriesForType(Type type)
            : base($"{type.AssemblyQualifiedName} has multiple user defined factories to build it.  A type can only have one factory defined.")
        {
        }
    }
}