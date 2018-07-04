using System;
using System.Collections.Generic;

namespace Dolittle.Dynamic
{
    /// <summary>
    /// An immutable property bag of key-value pairs
    /// </summary>
    public class PropertyBag : WriteOnceExpandoObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public PropertyBag(IDictionary<string,object> values) : base(values)
        {
        }
    }
}