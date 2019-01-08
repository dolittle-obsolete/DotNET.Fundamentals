using System;
using Dolittle.Collections;

namespace Dolittle.PropertyBags.Migrations
{

    /// <summary>
    /// Removes an existing Property
    /// </summary>
    public class RemoveProperty : MigrationChange
    {
        /// <summary>
        /// Instantiates an AddNewProperty migration change
        /// </summary>
        /// <param name="name">Action to be performed on the PropertyBag</param>
        public RemoveProperty(string name) : base(GetAction(name))
        {
        }

        static Action<NullFreeDictionary<string,object>> GetAction(string name)
        {
            return nfd => 
            {
                if(nfd == null)
                    throw new InvalidMigrationSource("NullFreeDictionary cannot be null");  


                if(!nfd.ContainsKey(name))
                    return;

                nfd.Remove(name);
            };
        }
    }  
}