using System;
using System.Collections.Generic;
using System.Reflection;
using Dolittle.Concepts;
using Dolittle.Reflection;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Extensions for object
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Creates a <see cref="PropertyBag"/> from an object.
        /// Maps primitive properties and complex objects to <see cref="PropertyBag"/> recursively
        /// </summary>
        /// <param name="obj">Object to convert</param>
        /// <returns></returns>
        public static PropertyBag ToPropertyBag(this object obj)
        {
            if(obj == null)
                return null;
            
            Dictionary<string,object> values = new Dictionary<string, object>();

            foreach (var property in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var value = property.PropertyType.IsAPrimitiveType() ? property.GetValue(obj) : property.PropertyType.IsConcept() ? property.GetValue(obj)?.GetConceptValue() : property.GetValue(obj).ToPropertyBag();
                values.Add(property.Name, value);
            }

            return new PropertyBag(values);    
        }
    }
}