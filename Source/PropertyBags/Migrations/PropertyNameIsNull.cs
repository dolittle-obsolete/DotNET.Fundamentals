// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// Exception that gets thrown when a property has a null name.
    /// </summary>
    public class PropertyNameIsNull : ArgumentNullException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyNameIsNull"/> class.
        /// </summary>
        public PropertyNameIsNull()
            : base($"Property name can't be null")
        {
        }
    }
}