// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.ApplicationModel
{
    /// <summary>
    /// Represents the concept of a microservice.
    /// </summary>
    public class Microservice : ConceptAs<Guid>
    {
        /// <summary>
        /// Represents the identifier for a not set microservice.
        /// </summary>
        public static readonly Microservice NotSet = Guid.Parse("4a5d2bc3-543f-459a-ab0b-e8e924093260");

        /// <summary>
        /// Implicitly converts from a <see cref="Guid"/> to a <see cref="Microservice"/>.
        /// </summary>
        /// <param name="microservice"><see cref="Guid"/> representing the microservice.</param>
        public static implicit operator Microservice(Guid microservice)
        {
            return new Microservice { Value = microservice };
        }

        /// <summary>
        /// Create a new <see cref="Microservice"/> identifier.
        /// </summary>
        /// <returns><see cref="Microservice"/>.</returns>
        public static Microservice New()
        {
            return new Microservice { Value = Guid.NewGuid() };
        }
    }
}