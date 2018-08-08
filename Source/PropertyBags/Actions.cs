/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

namespace Dolittle.PropertyBags
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using Dolittle.Execution;
    using Dolittle.Reflection;
    using Dolittle.Collections;
    using Dolittle.Strings;
    using Dolittle.Concepts;

    /// <summary>
    /// Helpers for building Actions that can be used in the PropertyBag factories
    /// </summary>
    public static class Actions 
    {
        /// <summary>
        /// Creates an Action that can be used to set a property
        /// </summary>
        /// <param name="targetType">Type that contains the Property</param>
        /// <param name="property">A PropertyInfo for the property to set</param>
        /// <returns>An action that can be used to set the specified property on an instance of the type</returns>
        public static Action<object, object> GetPropertySetter(Type targetType, PropertyInfo property)
        {
            var target = Expression.Parameter(typeof (object), "obj");
            var value = Expression.Parameter(typeof (object), "value");
            var body = Expression.Assign(
                Expression.Property(Expression.Convert(target, property.DeclaringType), property),
                Expression.Convert(value, property.PropertyType));

            var lambda = Expression.Lambda<Action<object, object>>(body, target, value);
            return lambda.Compile();
        }
    }
}