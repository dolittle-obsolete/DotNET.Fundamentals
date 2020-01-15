// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.DependencyInversion.Strategies
{
    /// <summary>
    /// Represents an <see cref="IActivationStrategy"/> that is null.
    /// </summary>
    public class Null : IActivationStrategy
    {
        /// <summary>
        /// Gets the null target.
        /// </summary>
        public object Target => null;

        /// <inheritdoc/>
        public System.Type GetTargetType()
        {
            return typeof(object);
        }
    }
}