// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Collections;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// A change of the PropertyBag that is to be performed as part of a migration.
    /// </summary>
    public abstract class MigrationChange
    {
        readonly Action<NullFreeDictionary<string, object>> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationChange"/> class.
        /// </summary>
        /// <param name="action">Action to be performed on the PropertyBag.</param>
        protected MigrationChange(Action<NullFreeDictionary<string, object>> action)
        {
            _action = action;
        }

        /// <summary>
        /// Perform a change.
        /// </summary>
        /// <param name="source">Source to change.</param>
        public void Perform(NullFreeDictionary<string, object> source)
        {
            _action?.Invoke(source);
        }
    }
}