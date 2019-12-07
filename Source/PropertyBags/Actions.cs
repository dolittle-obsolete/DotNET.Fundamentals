// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Helpers for building Actions that can be used in the PropertyBag factories.
    /// </summary>
    public static class Actions
    {
        /// <summary>
        /// Creates an Action that can be used to set a property.
        /// </summary>
        /// <param name="property">A PropertyInfo for the property to set.</param>
        /// <returns>An action that can be used to set the specified property on an instance of the type.</returns>
        public static Action<object, object> GetPropertySetter(PropertyInfo property)
        {
            var target = Expression.Parameter(typeof(object), "obj");
            var value = Expression.Parameter(typeof(object), "value");
            var body = Expression.Assign(
                Expression.Property(Expression.Convert(target, property.DeclaringType), property),
                Expression.Convert(value, property.PropertyType));

            var lambda = Expression.Lambda<Action<object, object>>(body, target, value);
            return lambda.Compile();
        }
    }
}