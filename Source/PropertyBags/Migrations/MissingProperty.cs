// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// Exception that gets thrown when a property is missing from the Migration Source.
    /// </summary>
    public class MissingProperty : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingProperty"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public MissingProperty(string propertyName)
            : base($"{propertyName} does not exist")
        {
        }
    }
}