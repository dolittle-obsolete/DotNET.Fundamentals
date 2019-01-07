using System;
using Dolittle.Collections;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// A change of the PropertyBag that is to be performed as part of a migration
    /// </summary>
    public abstract class MigrationChange
    {
        private Action<NullFreeDictionary<string,object>> _action;

        /// <summary>
        /// Instantiates a Change
        /// </summary>
        /// <param name="action">Action to be performed on the PropertyBag</param>
        protected MigrationChange(Action<NullFreeDictionary<string,object>> action)
        {
            _action = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public void Perform(NullFreeDictionary<string,object> source)
        {
            if(_action != null)
                _action(source);

        }
    }
}