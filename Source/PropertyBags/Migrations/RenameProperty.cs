// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Collections;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// Renames an existing Property.
    /// </summary>
    public class RenameProperty : MigrationChange
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenameProperty"/> class.
        /// </summary>
        /// <param name="originalName">Property to be renamed.</param>
        /// <param name="newName">New name for the property.</param>
        public RenameProperty(string originalName, string newName)
            : base(GetAction(originalName, newName))
        {
        }

        static Action<NullFreeDictionary<string, object>> GetAction(string originalName, string newName)
        {
            return nfd =>
            {
                if (nfd == null)
                    throw new InvalidMigrationSource("NullFreeDictionary cannot be null");

                if (!nfd.ContainsKey(originalName))
                    throw new MissingProperty($"{originalName ?? "[NULL]"} does not exist");

                if (newName == null)
                    throw new NullPropertyName("New property name cannot be null");

                if (nfd.ContainsKey(newName))
                    throw new DuplicateProperty($"{newName ?? "[NULL]"} already exists");

                if (!PropertyNameValidator.IsValid(newName))
                    throw new InvalidPropertyName($"New name '{newName ?? "[NULL]"}' is not a valid property name");

                var existingValue = nfd[originalName];
                nfd.Remove(originalName);
                nfd.Add(newName, existingValue);
            };
        }
    }
}