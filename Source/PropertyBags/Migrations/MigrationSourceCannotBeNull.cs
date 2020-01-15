// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// Exception that gets thrown when the Migration Source is not valid.
    /// </summary>
    public class MigrationSourceCannotBeNull : ArgumentNullException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationSourceCannotBeNull"/> class.
        /// </summary>
        /// <param name="sourceName">Name of the source.</param>
        public MigrationSourceCannotBeNull(string sourceName)
            : base($"A MigrationSource with name '{sourceName}' was null; typically a NullFreeDictionary - this is not allowed")
        {
        }
    }
}