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
        /// <param name="message">A message describing the exception.</param>
        public InvalidPropertyName(string message)
            : base(message)
        {
        }
    }
}
