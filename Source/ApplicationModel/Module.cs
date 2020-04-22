// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.ApplicationModel
{
    /// <summary>
    /// Represents the concept of a module.
    /// </summary>
    public class Module : ConceptAs<Guid>
    {
        /// <summary>
        /// Implicitly converts from a <see cref="Guid"/> to a <see cref="Module"/>.
        /// </summary>
        /// <param name="module"><see cref="Guid"/> representing the module.</param>
        public static implicit operator Module(Guid module)
        {
            return new Module { Value = module };
        }
    }
}
