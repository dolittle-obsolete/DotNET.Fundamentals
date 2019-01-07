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
        public AddNewProperty(string name, object value) : base(GetAction(name,value))
        {
        }

        static Action<NullFreeDictionary<string,object>> GetAction(string name, object value)
        {
            return nfd => 
            {
                if(nfd == null)
                    throw new InvalidMigrationSource("NullFreeDictionary cannot be null");  

                if(value == null)
                    return;

                if(nfd.ContainsKey(name))
                    throw new DuplicateProperty($"Property {name ?? "[NULL]" } already exists on this target");

                nfd.Add(name,(T)value);
            };
        }
    }    
}