/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Collections;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// Adds a new Property
    /// </summary>
    public class AddNewProperty<T> : MigrationChange
    {
        /// <summary>
        /// Instantiates an AddNewProperty migration change
        /// </summary>
        /// <param name="name">Action to be performed on the PropertyBag</param>
        /// <param name="value"></param>
        public AddNewProperty(string name, T value) : base(GetAction(name,value))
        {
        }

        static Action<NullFreeDictionary<string,object>> GetAction(string name, T value)
        {
            return nfd => 
            {
                if(nfd == null)
                    throw new InvalidMigrationSource("NullFreeDictionary cannot be null");  

                if(value == null)
                    return;

                if(name == null)
                    throw new NullPropertyName("Cannot add a property with the name NULL");

                if(!PropertyNameValidator.IsValid(name))
                    throw new InvalidPropertyName($"Property {name ?? "[NULL]" } is not a valid identifier");

                if(nfd.ContainsKey(name))
                    throw new DuplicateProperty($"Property {name ?? "[NULL]" } already exists on this target");

                nfd.Add(name,typeof(T).GetPropertyBagObjectValue(value));
            };
        }
    }   
}