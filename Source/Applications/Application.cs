﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents the concept of an application.
    /// </summary>
    public class Application : ConceptAs<Guid>
    {
        /// <summary>
        /// Represents the identifier for a not set application.
        /// </summary>
        public static readonly Application NotSet = Guid.Parse("4fe9492c-1d19-4e6b-be72-03208789906e");

        /// <summary>
        /// Implicitly converts from a <see cref="Guid"/> to a <see cref="Application"/>.
        /// </summary>
        /// <param name="application"><see cref="Guid"/> representing the application.</param>
        public static implicit operator Application(Guid application)
        {
            return new Application { Value = application };
        }

        /// <summary>
        /// Create a new <see cref="Application"/>identifier.
        /// </summary>
        /// <returns><see cref="Application"/>.</returns>
        public static Application New()
        {
            return new Application { Value = Guid.NewGuid() };
        }
    }
}