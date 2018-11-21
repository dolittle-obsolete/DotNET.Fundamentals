/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dolittle.Immutability
{
    /// <summary>
    /// Holds extension methods for working with immutables
    /// </summary>
    public static class ImmutableExtensions
    {
        /// <summary>
        /// Check if a type is immutable by virtue of it having public properties or fields that can be written to
        /// </summary>
        /// <param name="type"><see cref="Type"/> to check</param>
        /// <returns>True if it is immutable, false if not</returns>
        public static bool IsImmutable(this Type type)
        {
            var writeableProperties = type.GetWriteableProperties();
            var writeableFields = type.GetWriteableFields();

            return writeableProperties.Length == 0 && writeableFields.Length == 0;
        }

        /// <summary>
        /// Check if a type is really immutable and if it's not, throw the appropriate exception
        /// </summary>
        /// <param name="type"><see cref="Type"/> to check</param>
        public static void ShouldBeImmutable(this Type type)
        {
            var writeableProperties = type.GetWriteableProperties();
            if( writeableProperties.Length > 0 ) throw new WriteableImmutablePropertiesFound(type, writeableProperties);

            var writeableFields = type.GetWriteableFields();
            if( writeableFields.Length > 0 ) throw new WriteableImmutableFieldsFound(type, writeableFields);
        }


        /// <summary>
        /// Get the writeable properties - if any - on a specific type
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get from</param>
        /// <returns>Writeable <see cref="PropertyInfo">properties</see></returns>
        public static PropertyInfo[] GetWriteableProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public|BindingFlags.Instance)
                        .Where(_ => _.CanWrite).ToArray();
        }

        /// <summary>
        /// Get the writeable fields - if any - on a specific type
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get from</param>
        /// <returns>Writeable <see cref="FieldInfo">fields</see></returns>
        public static FieldInfo[] GetWriteableFields(this Type type)
        {
             return type .GetFields(BindingFlags.Public|BindingFlags.Instance)
                            .Where(_ => !_.Attributes.HasFlag(FieldAttributes.InitOnly)).ToArray();
        }
    }    
}