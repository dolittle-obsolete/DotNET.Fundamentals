// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// Exception that gets thrown when the Migration Source is not valid.
    /// </summary>
    public class InvalidMigrationSource : ArgumentNullException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidMigrationSource"/> class.
        /// </summary>
        /// <param name="message">A message describing the exception.</param>
        public InvalidMigrationSource(string message)
            : base(message)
        {
        }
    }
}