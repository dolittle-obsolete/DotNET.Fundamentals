/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.PropertyBags
{
    using System;
    using System.Linq;
    using System.Reflection;

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
    }
}