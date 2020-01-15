// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents the concept of a feature.
    /// </summary>
    public class Feature : ConceptAs<Guid>
    {
        /// <summary>
        /// Implicitly converts from a <see cref="Guid"/> to a <see cref="Feature"/>.
        /// </summary>
        /// <param name="feature"><see cref="Guid"/> representing the feature.</param>
        public static implicit operator Feature(Guid feature)
        {
            return new Feature { Value = feature };
        }
    }
}