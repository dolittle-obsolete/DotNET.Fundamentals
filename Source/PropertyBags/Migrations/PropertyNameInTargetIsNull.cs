// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// Exception that gets thrown when a property has a null name.
    /// </summary>
    public class PropertyNameInTargetIsNull : ArgumentNullException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyNameInTargetIsNull"/> class.
        /// </summary>
        /// <param name="sourcePropertyName">Name of the property.</param>
        public PropertyNameInTargetIsNull(string sourcePropertyName)
            : base($"New property name cannot be null - old name was {sourcePropertyName}")
        {
        }
    }
}