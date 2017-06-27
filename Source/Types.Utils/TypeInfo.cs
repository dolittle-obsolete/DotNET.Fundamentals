/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Reflection;

namespace doLittle.Types.Utils
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypeInfo"/>
    /// </summary>
    /// <typeparam name="T">Type it holds info for</typeparam>
    public class TypeInfo<T> : ITypeInfo
    {
        /// <summary>
        /// Gets a singleton instance of the TypeInfo
        /// </summary>
        public static readonly TypeInfo<T> Instance = new TypeInfo<T>();

        TypeInfo()
        {
            var type = typeof(T); 
            var typeInfo = type.GetTypeInfo();

            var defaultConstructor = typeInfo.DeclaredConstructors.Any(c=>c.GetParameters().Length == 0);
            
            HasDefaultConstructor = 
                typeInfo.IsValueType ||
                defaultConstructor;
        }

        /// <inheritdoc/>
        public bool HasDefaultConstructor { get; }
    }
}
