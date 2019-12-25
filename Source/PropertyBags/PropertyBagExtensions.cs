// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using Dolittle.Concepts;
using Dolittle.Reflection;
using Dolittle.Strings;
using Dolittle.Time;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// Extensions for <see cref="PropertyBag"/>.
    /// </summary>
    public static class PropertyBagExtensions
    {
        /// <summary>
        /// Constructs an instance from a <see cref="PropertyBag"/> based on the information present
        /// in the <see cref="PropertyInfo"/>.
        /// </summary>
        /// <param name="propertyBag"><see cref="PropertyBag"/> to construct from.</param>
        /// <param name="propertyInfo"><see cref="PropertyInfo"/>.</param>
        /// <param name="factory"><see cref="IObjectFactory"/>.</param>
        /// <returns>Dynamic instance.</returns>
        public static dynamic ConstructInstanceOfType(this PropertyBag propertyBag, PropertyInfo propertyInfo, IObjectFactory factory)
        {
            return ConstructInstanceOfType(propertyBag, propertyInfo.PropertyType, propertyInfo.Name, factory);
        }

        /// <summary>
        /// Constructs an instance from a <see cref="PropertyBag"/> based on the information present in the <see cref="ParameterInfo"/>.
        /// </summary>
        /// <param name="propertyBag"><see cref="PropertyBag"/> to construct from.</param>
        /// <param name="parameterInfo"><see cref="ParameterInfo"/>.</param>
        /// <param name="factory"><see cref="IObjectFactory"/>.</param>
        /// <returns>Dynamic instance.</returns>
        public static dynamic ConstructInstanceOfType(this PropertyBag propertyBag, ParameterInfo parameterInfo, IObjectFactory factory)
        {
            return ConstructInstanceOfType(propertyBag, parameterInfo.ParameterType, parameterInfo.Name.ToPascalCase(), factory);
        }

        static dynamic ConstructInstanceOfType(PropertyBag pb, Type targetType, string pbKey, IObjectFactory factory)
        {
            if (!pb.ContainsKey(pbKey))
                return null;

            var value = pb[pbKey];
            if (value == null)
                return null;
            if (targetType.IsDate())
                return BuildDate(value);
            if (targetType.IsDateTimeOffset())
                return BuildDateTimeOffset(value);
            if (targetType.IsAPrimitiveType() || targetType == typeof(PropertyBag))
                return targetType == typeof(PropertyBag) ? (PropertyBag)value : value;
            if (targetType.IsConcept())
                return ConceptFactory.CreateConceptInstance(targetType, value);
            if (targetType.IsEnumerable())
                return targetType.ConstructEnumerable(factory, value);

            return factory.Build(targetType, value as PropertyBag);
        }

        static DateTime BuildDate(object value)
        {
            return ((long)value).ToDateTime();
        }

        static DateTimeOffset BuildDateTimeOffset(object value)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds((long)value);
        }
    }
}