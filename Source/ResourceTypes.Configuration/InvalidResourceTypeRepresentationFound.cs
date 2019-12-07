// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// The exception that gets thrown when an invalid <see cref="IRepresentAResourceType"/> is discovered.
    /// </summary>
    public class InvalidResourceTypeRepresentationFound : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidResourceTypeRepresentationFound"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public InvalidResourceTypeRepresentationFound(string message)
            : base(message)
        {
        }
    }
}