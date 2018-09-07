using System;
using System.Reflection;
using Dolittle.Concepts;
using Dolittle.Reflection;
using Dolittle.Strings;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// 
    /// </summary>
    public static class PropertyBagExtensions
    {
        /// <summary>
        /// Constructs an instance from a <see cref="PropertyBag"/> based on the information present in the <see cref="PropertyInfo"/>
        /// </summary>
        /// <param name="propertyBag"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static dynamic ConstructInstanceOfType(this PropertyBag propertyBag, PropertyInfo propertyInfo, IObjectFactory factory)
        {
            return ConstructInstanceOfType(propertyBag, propertyInfo.PropertyType, propertyInfo.Name, factory);
        }
        /// <summary>
        /// Constructs an instance from a <see cref="PropertyBag"/> based on the information present in the <see cref="ParameterInfo"/>
        /// </summary>
        /// <param name="propertyBag"></param>
        /// <param name="parameterInfo"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static dynamic ConstructInstanceOfType(this PropertyBag propertyBag, ParameterInfo parameterInfo, IObjectFactory factory)
        {
            return ConstructInstanceOfType(propertyBag, parameterInfo.ParameterType, parameterInfo.Name.ToPascalCase(), factory);
        }
        
        static dynamic ConstructInstanceOfType(PropertyBag pb, Type targetType, string pbKey, IObjectFactory factory)
        {
            if (!pb.ContainsKey(pbKey))
                return null;
            
            var value = pb[pbKey];
            if(value == null)
                return null;
            if(targetType.IsAPrimitiveType() || targetType == typeof(PropertyBag))
                return targetType == typeof(PropertyBag)? (PropertyBag)value : value;
            if(targetType.IsConcept())
                return ConceptFactory.CreateConceptInstance(targetType, value);
            if (targetType.IsEnumerable())
                return targetType.ConstructEnumerable(factory, value);
            
            return factory.Build(targetType, value as PropertyBag);
        }
    }
}