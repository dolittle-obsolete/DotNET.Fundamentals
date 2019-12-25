// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents the concept of a bounded context.
    /// </summary>
    public class BoundedContext : ConceptAs<Guid>
    {
        /// <summary>
        /// Represents the identifier for a not set application.
        /// </summary>
        public static readonly BoundedContext NotSet = Guid.Parse("68ed1213-c025-4d73-8767-d54cc1dba090");

        /// <summary>
        /// Implicitly converts from a <see cref="Guid"/> to a <see cref="BoundedContext"/>.
        /// </summary>
        /// <param name="boundedContext"><see cref="Guid"/> representing the bounded context.</param>
        public static implicit operator BoundedContext(Guid boundedContext)
        {
            return new BoundedContext { Value = boundedContext };
        }

        /// <summary>
        /// Create a new <see cref="BoundedContext"/>identifier.
        /// </summary>
        /// <returns><see cref="BoundedContext"/>.</returns>
        public static BoundedContext New()
        {
            return new BoundedContext { Value = Guid.NewGuid() };
        }
    }
}