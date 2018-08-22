/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dolittle.Reflection
{
    /// <summary>
    /// Provides a set of methods for working with <see cref="Type">types</see>
    /// </summary>
    public static class TypeExtensions
    {
        static HashSet<Type> AdditionalPrimitiveTypes = new HashSet<Type>
            {
                typeof(decimal),typeof(string),typeof(Guid),typeof(DateTime),typeof(DateTimeOffset),typeof(TimeSpan)
            }; 

        static HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(byte), typeof(sbyte),
            typeof(short), typeof(int), typeof(long),
            typeof(ushort), typeof(uint), typeof(ulong),
            typeof(double), typeof(decimal), typeof(Single)
        };

        static ITypeInfo GetTypeInfo(Type type)
        {
            var typeInfoType = typeof(TypeInfo<>).MakeGenericType(type);
            var instanceField = typeInfoType.GetTypeInfo().GetField("Instance", BindingFlags.Public | BindingFlags.Static);
            var typeInfo =  instanceField.GetValue(null) as ITypeInfo;
            return typeInfo;
        }


        /// <summary>
        /// Check if a type has an attribute associated with it
        /// </summary>
        /// <typeparam name="T">Type to check</typeparam>
        /// <returns>True if there is an attribute, false if not</returns>
        public static bool HasAttribute<T>(this Type type) where T : Attribute
        {
            var attributes = type.GetTypeInfo().GetCustomAttributes(typeof(T), false).ToArray();
            return attributes.Length == 1;
        }

        /// <summary>
        /// Check if a type is nullable or not
        /// </summary>
        /// <param name="type"><see cref="Type"/> to check</param>
        /// <returns>True if type is nullable, false if not</returns>
        public static bool IsNullable(this Type type)
        {
            return (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Check if a type is a number or not
        /// </summary>
        /// <param name="type"><see cref="Type"/> to check</param>
        /// <returns>True if type is numeric, false if not</returns>
        public static bool IsNumericType(this Type type)
        {
            return NumericTypes.Contains(type) ||
                   NumericTypes.Contains(Nullable.GetUnderlyingType(type));
        }

        /// <summary>
        /// Check if a type is a Date or not
        /// </summary>
        /// <param name="type"><see cref="Type"/> to check</param>
        /// <returns>True if type is a date, false if not</returns>
        public static bool IsDate(this Type type)
        {
            return type == typeof (DateTime) || Nullable.GetUnderlyingType(type) == typeof (DateTime);
        }

        /// <summary>
        /// Check if a type is a DateTimeOffset or not
        /// </summary>
        /// <param name="type"><see cref="Type"/> to check</param>
        /// <returns>True if type is a date, false if not</returns>
        public static bool IsDateTimeOffset(this Type type)
        {
            return type == typeof (DateTimeOffset) || Nullable.GetUnderlyingType(type) == typeof (DateTimeOffset);
        }
        

        /// <summary>
        /// Check if a type is a Boolean or not
        /// </summary>
        /// <param name="type"><see cref="Type"/> to check</param>
        /// <returns>True if type is a boolean, false if not</returns>
        public static bool IsBoolean(this Type type)
        {
            return type == typeof (Boolean) || Nullable.GetUnderlyingType(type) == typeof (Boolean);
        }

        /// <summary>
        /// Check if a type has a default constructor that does not take any arguments
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns>true if it has a default constructor, false if not</returns>
        public static bool HasDefaultConstructor(this Type type)
        {
            return GetTypeInfo(type).HasDefaultConstructor || type.GetConstructors().Any(c => c.GetParameters().Length == 0);
        }


        /// <summary>
        /// Check if a type has a non default constructor
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns>true if it has a non default constructor, false if not</returns>
        public static bool HasNonDefaultConstructor(this Type type)
        {
            return type.GetConstructors().Any(c => c.GetParameters().Length > 0);
        }


        /// <summary>
        /// Get the default constructor from a type
        /// </summary>
        /// <param name="type">Type to get from</param>
        /// <returns>The default <see cref="ConstructorInfo"/></returns>
        public static ConstructorInfo GetDefaultConstructor(this Type type)
        {
            return type.GetConstructors().Where(c => c.GetParameters().Length == 0).Single();
        }

        /// <summary>
        /// Get the non default constructor, assuming there is only one
        /// </summary>
        /// <remarks></remarks>
        /// <param name="type">Type to get from</param>
        /// <returns>The <see cref="ConstructorInfo"/> for the constructor</returns>
        public static ConstructorInfo GetNonDefaultConstructor(this Type type)
        {
            return type.GetConstructors().Where(c => c.GetParameters().Length > 0).Single();
        }

        /// <summary>
        /// Get the non default constructor matching the types
        /// </summary>
        /// <remarks></remarks>
        /// <param name="type">Type to get from</param>
        /// <param name="parameterTypes">Types for matching the parameters</param>
        /// <returns>The <see cref="ConstructorInfo"/> for the constructor</returns>
        public static ConstructorInfo GetNonDefaultConstructor(this Type type, Type[] parameterTypes)
        {
            return type.GetTypeInfo().GetConstructor(parameterTypes);
        }

        /// <summary>
        /// Get the non default constructor with the greatest number of parameters.
        /// Should be used with care. Constructors are not ordered, so if there are multiple constructors with the
        /// same number of parameters, it is indeterminate which will be returned.
        /// </summary>
        /// <param name="type">Type to get from</param>
        /// <returns>The <see cref="ConstructorInfo"/> for the constructor</returns>
        public static ConstructorInfo GetNonDefaultConstructorWithGreatestNumberOfParameters(this Type type)
        {
            return type.GetTypeInfo()
                            .DeclaredConstructors.Where(c => c.GetParameters().Length > 0)
                            .OrderByDescending((t) => t.GetParameters().Length)
                            .FirstOrDefault();
        }

        /// <summary>
        /// Gets all the public properties with setters
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetSettableProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite).ToArray();
        }

        /// <summary>
        /// Check if a type implements a specific interface
        /// </summary>
        /// <typeparam name="T">Interface to check for</typeparam>
        /// <param name="type">Type to check</param>
        /// <returns>True if the type implements the interface, false if not</returns>
        public static bool HasInterface<T>(this Type type)
        {
            var hasInterface = type.HasInterface(typeof (T));
            return hasInterface;
        }

        /// <summary>
        /// Check if a type implements a specific interface
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <param name="interfaceType">Interface to check for</param>
        /// <returns>True if the type implements the interface, false if not</returns>
        public static bool HasInterface(this Type type, Type interfaceType)
        {
            var hasInterface = type.GetTypeInfo().ImplementedInterfaces.Where(t => $"{t.Namespace}.{t.Name}" == $"{interfaceType.Namespace}.{interfaceType.Name}").Count() == 1;
            return hasInterface;
        }

        /// <summary>
        /// Check if a type derives from an open generic type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="openGenericType"></param>
        /// <returns></returns>
        public static bool IsDerivedFromOpenGeneric(this Type type, Type openGenericType)
        {
            var typeToCheck = type;
            while (typeToCheck != null && typeToCheck != typeof(object))
            {
                var currentType = typeToCheck.GetTypeInfo().IsGenericType ? typeToCheck.GetGenericTypeDefinition() : typeToCheck;
                if (openGenericType == currentType)
                {
                    return true;
                }
                typeToCheck = typeToCheck.GetTypeInfo().BaseType;
            }
            return false;
        }

        /// <summary>
        /// Check if a type implements an open generic type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="openGenericType"></param>
        /// <returns></returns>
        public static bool ImplementsOpenGeneric(this Type type, Type openGenericType)
        {
            var openGenericTypeInfo = openGenericType.GetTypeInfo();
            var typeInfo = type.GetTypeInfo();
            
            return typeInfo.GetInterfaces()
                .Where(i => i.GetTypeInfo().IsGenericType) 
                .Where(i => i.GetTypeInfo().GetGenericTypeDefinition().GetTypeInfo() == openGenericTypeInfo)
                .Any();
        }
        /// <summary>
        /// Check if a type is a "primitve" type.  This is not just dot net primitives but basic types like string, decimal, datetime,
        /// that are not classified as primitive types.
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns>True if a "primitive"</returns>
        public static bool IsAPrimitiveType(this Type type)
        {
            return type.GetTypeInfo().IsPrimitive 
                    || type.IsNullable() || AdditionalPrimitiveTypes.Contains(type);
        }


        /// <summary>
        /// Check if a type implements another type - supporting interfaces, abstract types, with or without generics
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <param name="super">Super / parent type to check against</param>
        /// <returns>True if derived, false if not</returns>
        public static bool Implements(this Type type, Type super)
        {
            return type.AllBaseAndImplementingTypes().Contains(super);
        }

        /// <summary>
        /// Returns all base types of a given type, both open and closed generic (if any), including itself.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> AllBaseAndImplementingTypes(this Type type)
        {
            return type.BaseTypes()
                .Concat(type.GetTypeInfo().GetInterfaces())
                .SelectMany(ThisAndMaybeOpenType)
                .Where(t=>t != type && t != typeof(Object));
        }

        /// <summary>
        /// Indicates whether the Type represents an "immutable" type
        /// </summary>
        /// <remarks>
        /// Immutability is a difficult concept in C#.  Things can be changed via reflection, fields rather than properties, private setters, etc.static
        /// We are taking a deliberately limited view of immutability.  In this case it simply means an object that has no properties with setters (public or private)
        /// This is not intended to be an indication that the object is truly immutable, instead it's to guide the instantiation strategy when we create and hydrate it
        /// from a serialized form (e.g. PropertyBag)
        /// </remarks>
        /// <param name="type">The type to check</param>
        /// <returns>true if immutable, false otherwise</returns>
        public static bool IsImmutable(this Type type)
        {
            return !type.GetSettableProperties().Any();
        }

        /// <summary>
        /// Indicates whether the Type has any public properties to get or set state
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <returns>True if there are public properties (get or set), false otherwise</returns>
        public static bool HasVisibleProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Any();
        }

        static IEnumerable<Type> BaseTypes(this Type type)
        {
            var currentType = type;
            while (currentType != null)
            {
                yield return currentType;
                currentType = currentType.GetTypeInfo().BaseType;
            }
        }

        static IEnumerable<Type> ThisAndMaybeOpenType(Type type)
        {
            yield return type;
            if (type.GetTypeInfo().IsGenericType && !type.GetTypeInfo().ContainsGenericParameters)
            {
                yield return type.GetGenericTypeDefinition();
            }
        }
    }
}
