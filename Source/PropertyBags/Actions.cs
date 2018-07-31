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

    public static class Actions 
    {
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