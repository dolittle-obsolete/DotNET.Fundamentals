/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.PropertyBags
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Dolittle.Concepts;
    using Dolittle.Reflection;

    /// <summary>
    /// Extensions for Type to help with <see cref="PropertyBag" />
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks if the Type has a constructor with a single PropertyBag parameter
        /// </summary>
        /// <param name="type">The Type to check</param>
        /// <returns>true if it has a constructor with a single PropertyBag parameter, false otherwise</returns>
        public static bool HasPropertyBagConstructor(this Type type)
        {
            var ctor = type.GetTypeInfo().DeclaredConstructors.SingleOrDefault(c => c.GetParameters().Length == 1 
                                                                                && c.GetParameters().First().ParameterType == typeof(PropertyBag));
            return ctor != null;
        }

        /// <summary>
        /// Gets the single PropertyBag constuctor
        /// </summary>
        /// <param name="type">Type to get the constructor from</param>
        /// <returns>The ConstructorInfo if present, otherwise throws an exception</returns>
        public static ConstructorInfo GetPropertyBagConstructor(this Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors.SingleOrDefault(c => c.GetParameters().Length == 1 
                                                                                && c.GetParameters().First().ParameterType == typeof(PropertyBag));
        }

        /// <summary>
        /// Constructs a generic List based on the type of the enumerable and the input object
        /// </summary>
        /// <param name="enumerableType">The List's type</param>
        /// <param name="factory">The <see cref="IObjectFactory"/> for creating the list's objects</param>
        /// <param name="obj">The object from which the list's element will be built of</param>
        /// <returns></returns>
        public static dynamic ConstructEnumerable(this Type enumerableType, IObjectFactory factory, object obj)
        {
            ThrowIfNotEnumerableType(enumerableType);
            ThrowIfObjectIsNotEnumerable(obj);
            var elementType = enumerableType.GetEnumerableElementType();
            dynamic list = Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
            
            foreach (object element in obj as IEnumerable)
            {
                if (element == null) 
                    list.Add(null);
                else if (element.GetType().Equals(typeof(PropertyBag)) || !element.GetType().IsAPrimitiveType()) 
                {
                    var method = typeof(IObjectFactory).GetMethods(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance).FirstOrDefault(m => m.Name.Equals("Build") && m.GetGenericArguments().Count() == 1);
                    ThrowIfGenericMethodNotFound(method);
                    method = method.MakeGenericMethod(new Type[] {elementType});
                    dynamic actualValue = method.Invoke(factory, new object[] {element as PropertyBag});
                    list.Add(actualValue);
                }
                else
                    list.Add((dynamic)element);
            }
            return list.ToArray();
            
        }

        /// <summary>
        /// Constructs the <see cref="object">obj</see> as an object suitable for a <see cref="PropertyBag"/>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object GetPropertyBagObjectValue(this Type type, object obj)
        {
            if(type.IsEnumerable())
                return type.ConstructEnumerableForPropertyBag(obj);

            if(type.IsConcept())
                return obj?.GetConceptValue();

            if(type.IsDate() || type.IsDateTimeOffset())
                return GetDateAsUnixTime(type,obj);

            if(type.IsAPrimitiveType())
                return obj;
            
            return obj.ToPropertyBag();
        }

        static long? GetDateAsUnixTime(Type type, object obj)
        {
            try
            {
                if(type.IsNullable() && obj == null)
                    return (long?)null;

                return type.IsDate() ? new DateTimeOffset(((DateTime)obj).ToUniversalTime()).ToUniversalTime().ToUnixTimeMilliseconds() : ((DateTimeOffset)obj).ToUnixTimeMilliseconds();
            }
            catch(Exception ex)
            {
                Console.WriteLine(obj.ToString());
                Console.WriteLine(ex);
                throw;
            }
            
        }


        /// <summary>
        /// Constructs an <see cref="IEnumerable"/> as an object suitable for a <see cref="PropertyBag"/>
        /// </summary>
        /// <param name="propType"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object ConstructEnumerableForPropertyBag(this Type propType, object obj)
        {
            if (obj == null) return null;
            if (propType.ImplementsOpenGeneric(typeof(IDictionary<,>)))
                throw new ArgumentException("property type cannot be Dictionary<,>");
            var elementType = propType.GetEnumerableElementType();
            var enumerableObject = obj as IEnumerable;

            if (enumerableObject == null) return null;
            
            var resultList = new List<object>();
            foreach (var element in enumerableObject)
                resultList.Add(elementType.GetPropertyBagObjectValue(element));

            return resultList.ToArray();
        }

        static void ThrowIfObjectIsNotEnumerable(object obj)
        {
            if ((obj as IEnumerable) == null) throw new ObjectIsNotEnumerable("The object is not enumerable");
        }

        static void ThrowIfNotEnumerableType(Type type)
        {
            if (!type.IsEnumerable()) throw new TypeIsNotEnumerable(type);
        }

        static void ThrowIfGenericMethodNotFound(MethodInfo method)
        {
            if (method == null) throw new GenericBuildMethodNotFound($"Generic method taking one generic argument called Build was not found in tbe {typeof(IObjectFactory).Name}");
        }
    }
}