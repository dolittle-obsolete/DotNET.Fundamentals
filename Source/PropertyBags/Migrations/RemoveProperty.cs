// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Collections;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// Removes an existing Property.
    /// </summary>
    public class RemoveProperty : MigrationChange
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveProperty"/> class.
        /// </summary>
        /// <param name="name">Action to be performed on the PropertyBag.</param>
        public RemoveProperty(string name)
            : base(GetAction(name))
        {
        }

        static Action<NullFreeDictionary<string, object>> GetAction(string name)
        {
            return nfd =>
            {
                if (nfd == null)
                    throw new MigrationSourceCannotBeNull(name);

                if (!nfd.ContainsKey(name))
                    return;

                nfd.Remove(name);
            };
        }
    }
}