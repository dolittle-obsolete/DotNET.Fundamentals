// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// Indicates that the Property is missing from the Migration Source.
    /// </summary>
    public class MissingProperty : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingProperty"/> class.
        /// </summary>
        /// <param name="message">A message describing the exception.</param>
        public MissingProperty(string message)
            : base(message)
        {
        }
    }
}