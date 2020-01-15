// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// Exception that gets thrown when a migration has duplicate properties.
    /// </summary>
    public class DuplicateProperty : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateProperty"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public DuplicateProperty(string propertyName)
            : base($"{propertyName} already exists")
        {
        }
    }
}