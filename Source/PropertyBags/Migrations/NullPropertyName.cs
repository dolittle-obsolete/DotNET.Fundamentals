// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// Exception that gets thrown when a property has a null name.
    /// </summary>
    public class NullPropertyName : ArgumentNullException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullPropertyName"/> class.
        /// </summary>
        /// <param name="message">A message describing the exception.</param>
        public NullPropertyName(string message)
            : base(message)
        {
        }
    }
}