/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace doLittle.Reflection
{
    /// <summary>
    /// Provides extension methods for converting any <see cref="object"/> to a <see cref="IDictionary"/>
    /// </summary>
    public static class ObjectToDictionaryExtensions
    {
        /// <summary>
        /// Convert an <see cref="object"/> to a <see cref="IDictionary"/>
        /// </summary>
        /// <param name="source"><see cref="Object"/> to turn into a dictionary</param>
        /// <returns><see cref="IDictionary"/> with all keys and values</returns>
        public static IDictionary<string, object> ToDictionary(this object source)
        {
            return source.ToDictionary<object>();
        }

        /// <summary>
        /// Convert an <see cref="object"/> to a <see cref="IDictionary"/>
        /// </summary>
        /// <param name="source"><see cref="Object"/> to turn into a dictionary</param>
        /// <returns><see cref="IDictionary"/> with all keys and values</returns>
        public static IDictionary<string, T> ToDictionary<T>(this object source)
        {
            if (source == null)
                ThrowExceptionIfSourceArgumentIsNull();

            var dictionary = new Dictionary<string, T>();
            foreach (var property in source.GetType().GetTypeInfo().GetProperties())
                AddPropertyToDictionary<T>(property, source, dictionary);

            return dictionary;
        }

        static void AddPropertyToDictionary<T>(PropertyInfo property, object source, Dictionary<string, T> dictionary)
        {
            object value = property.GetValue(source);
            if (IsOfType<T>(value))
                dictionary.Add(property.Name, (T)value);
        }

        static bool IsOfType<T>(object value)
        {
            return value is T;
        }

        static void ThrowExceptionIfSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }
    }
}