﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Concepts;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents the name of a <see cref="Feature"/>.
    /// </summary>
    public class FeatureName : ConceptAs<string>
    {
        /// <summary>
        /// Implicitly converts from a <see cref="string"/> to a <see cref="FeatureName"/>.
        /// </summary>
        /// <param name="featureName">Name of the feature.</param>
        public static implicit operator FeatureName(string featureName)
        {
            return new FeatureName { Value = featureName };
        }
    }
}
