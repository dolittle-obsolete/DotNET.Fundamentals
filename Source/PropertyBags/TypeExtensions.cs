// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Concepts;
using Dolittle.Reflection;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Extensions for Type to help with <see cref="PropertyBag" />.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks if the Type has a constructor with a single PropertyBag parameter.
        /// </summary>
        /// <param name="type">The Type to check.</param>
        /// <returns>true if it has a constructor with a single PropertyBag parameter, false otherwise.</returns>
        public static bool HasPropertyBagConstructor(this Type type)
        {
            var ctor = type.GetTypeInfo().DeclaredConstructors.SingleOrDefault(c => c.GetParameters().Length == 1
                                                                                && c.GetParameters().First().ParameterType == typeof(PropertyBag));
            return ctor != null;
        }

        /// <summary>
        /// Gets the single PropertyBag constuctor.
        /// </summary>
        /// <param name="type">Type to get the constructor from.</param>
        /// <returns>The ConstructorInfo if present, otherwise throws an exception.</returns>
        public static ConstructorInfo GetPropertyBagConstructor(this Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors.SingleOrDefault(c => c.GetParameters().Length == 1
                                                                                && c.GetParameters().First().ParameterType == typeof(PropertyBag));
        }

        /// <summary>
        /// Constructs a generic List based on the type of the enumerable and the input object.
        /// </summary>
        /// <param name="enumerableType">The List's type.</param>
        /// <param name="factory">The <see cref="IObjectFactory"/> for creating the list's objects.</param>
        /// <param name="source">The object from which the list's element will be built of.</param>
        /// <returns>Dynamic enumerable.</returns>
        public static dynamic ConstructEnumerable(this Type enumerableType, IObjectFactory factory, object source)
        {
            ThrowIfNotEnumerableType(enumerableType);
            ThrowIfObjectIsNotEnumerable(source);
            var elementType = enumerableType.GetEnumerableElementType();
            dynamic list = Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));

            foreach (object element in source as IEnumerable)
            {
                if (element == null)
                {
                    list.Add(null);
                }
                else if (element.GetType().Equals(typeof(PropertyBag)) || !element.GetType().IsAPrimitiveType())
                {
                    var method = Array.Find(typeof(IObjectFactory).GetMethods(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance), m => m.Name.Equals("Build", StringComparison.InvariantCulture) && m.GetGenericArguments().Length == 1);
                    ThrowIfGenericMethodNotFound(method);
                    method = method.MakeGenericMethod(new Type[] { elementType });
                    dynamic actualValue = method.Invoke(factory, new object[] { element as PropertyBag });
                    list.Add(actualValue);
                }
                else
                {
                    list.Add((dynamic)element);
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// Constructs the <see cref="object">obj</see> as an object suitable for a <see cref="PropertyBag"/>.
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get object form.</param>
        /// <param name="source">Source object.</param>
        /// <returns>Instance.</returns>
        public static object GetPropertyBagObjectValue(this Type type, object source)
        {
            if (type.IsEnumerable())
                return type.ConstructEnumerableForPropertyBag(source);

            if (type.IsConcept())
                return source?.GetConceptValue();

            if (type.IsDate() || type.IsDateTimeOffset())
                return GetDateAsUnixTime(type, source);

            if (type.IsAPrimitiveType())
                return source;

            return source.ToPropertyBag();
        }

        /// <summary>
        /// Constructs an <see cref="IEnumerable"/> as an object suitable for a <see cref="PropertyBag"/>.
        /// </summary>
        /// <param name="propType">Type of property value.</param>
        /// <param name="source">Source object.</param>
        /// <returns>Instance.</returns>
        public static object ConstructEnumerableForPropertyBag(this Type propType, object source)
        {
            if (source == null) return null;
            if (propType.ImplementsOpenGeneric(typeof(IDictionary<,>)))
                throw new ArgumentException("property type cannot be Dictionary<,>");
            var elementType = propType.GetEnumerableElementType();
            var enumerableObject = source as IEnumerable;

            if (enumerableObject == null) return null;

            var resultList = new List<object>();
            foreach (var element in enumerableObject)
                resultList.Add(elementType.GetPropertyBagObjectValue(element));

            return resultList.ToArray();
        }

        static long? GetDateAsUnixTime(Type type, object obj)
        {
            try
            {
                if (type.IsNullable() && obj == null)
                    return (long?)null;

                return type.IsDate() ? new DateTimeOffset(((DateTime)obj).ToUniversalTime()).ToUniversalTime().ToUnixTimeMilliseconds() : ((DateTimeOffset)obj).ToUnixTimeMilliseconds();
            }
            catch (Exception ex)
            {
                Console.WriteLine(obj.ToString());
                Console.WriteLine(ex);
                throw;
            }
        }

        static void ThrowIfObjectIsNotEnumerable(object obj)
        {
            if (!(obj is IEnumerable)) throw new ObjectIsNotEnumerable(obj.GetType());
        }

        static void ThrowIfNotEnumerableType(Type type)
        {
            if (!type.IsEnumerable()) throw new TypeIsNotEnumerable(type);
        }

        static void ThrowIfGenericMethodNotFound(MethodInfo method)
        {
            if (method == null) throw new GenericBuildMethodNotFound(typeof(IObjectFactory));
        }
    }
}