/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using Dolittle.Reflection;

namespace Dolittle.Concepts
{
    /// <summary>
    /// Factory to create an instance of a <see cref="ConceptAs{T}"/> from the Type and Underlying value.
    /// </summary>
    public class ConceptFactory
    {
        /// <summary>
        /// Creates an instance of a <see cref="ConceptAs{T}"/> given the type and underlying value.
        /// </summary>
        /// <param name="type">Type of the ConceptAs to create</param>
        /// <param name="value">Value to give to this instance</param>
        /// <returns>An instance of a ConceptAs with the specified value</returns>
        public static object CreateConceptInstance(Type type, object value)
        {            
            //var instance = Activator.CreateInstance(type);
            var val = new object();

            var valueProperty = type.GetTypeInfo().GetProperty("Value");

            var genericArgumentType = GetPrimitiveTypeConceptIsBasedOn(type);
            if (genericArgumentType == typeof (Guid))
            {
                val = value == null ? Guid.Empty : Guid.Parse(value.ToString());
            }

            if (valueProperty.PropertyType.IsAPrimitiveType())
            {
                val = value ?? Activator.CreateInstance(valueProperty.PropertyType);
            }

            if (valueProperty.PropertyType == typeof (string))
            {
                val = value ?? string.Empty;
            }
            
            if (valueProperty.PropertyType == typeof (DateTime))
            {
                val = value ?? default(DateTime);
            }

            if (valueProperty.PropertyType == typeof (DateTimeOffset))
            {
                val = value ?? default(DateTimeOffset);
            }

            if(IsGuidFromString(genericArgumentType,val))
            {
                val = val == null ? Guid.Empty : new Guid(val as string);
            }

            if (val.GetType() != genericArgumentType && !IsGuidFromString(genericArgumentType,val))
                val = Convert.ChangeType(val, genericArgumentType, null);

            object instance = null;
            if(type.HasDefaultConstructor()){
                instance = Activator.CreateInstance(type);
                valueProperty.SetValue(instance, val, null);
            } 
            else 
            {
                instance = type.GetNonDefaultConstructor().Invoke(new object[]{ val });
            }
            return instance;
        }

        static Type GetPrimitiveTypeConceptIsBasedOn(Type conceptType)
        {
            return ConceptMap.GetConceptValueType(conceptType);
        }

        static bool IsGuidFromString(Type genericArgumentType, object value)
        {
            return genericArgumentType == typeof(Guid) && value.GetType() == typeof(string);
        }
    }
}