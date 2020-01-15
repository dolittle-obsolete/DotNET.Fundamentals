// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// Exception that gets thrown when the Property Name being added is not a valid Property Name.
    /// </summary>
    public class InvalidPropertyName : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPropertyName"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        public InvalidPropertyName(string propertyName)
            : base($"{propertyName} is not a valid identifier")
        {
        }
    }
}
