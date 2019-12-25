// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Dolittle.Collections;
using Dolittle.Reflection;

namespace Dolittle.Mapping
{
    /// <summary>
    /// Represents a mapping description used by <see cref="IMapper"/>.
    /// </summary>
    /// <typeparam name="TSource">Source type for the map.</typeparam>
    /// <typeparam name="TTarget">Target type for the map.</typeparam>
    public abstract class Map<TSource, TTarget> : IMap
    {
        readonly List<PropertyMap<TSource, TTarget>> _properties = new List<PropertyMap<TSource, TTarget>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Map{TSource, TTarget}"/> class.
        /// </summary>
        protected Map()
        {
            AddDefaultPropertyMaps();
        }

        /// <summary>
        /// Gets the properties mapped.
        /// </summary>
        public IEnumerable<PropertyMap<TSource, TTarget>> Properties => _properties;

        /// <inheritdoc/>
        public Type Source => typeof(TSource);

        /// <inheritdoc/>
        public Type Target => typeof(TTarget);

        /// <inheritdoc/>
        IEnumerable<IPropertyMap> IMap.Properties => _properties;

        /// <summary>
        /// Describe a specific property.
        /// </summary>
        /// <param name="property">Expression representing the property.</param>
        /// <returns>A new map for the property.</returns>
        protected PropertyMap<TSource, TTarget> Property(Expression<Func<TSource, object>> property)
        {
            var propertyInfo = property.GetPropertyInfo();
            return AddPropertyMap(propertyInfo);
        }

        void AddDefaultPropertyMaps()
        {
            typeof(TSource).GetTypeInfo().GetProperties().ForEach(p => AddPropertyMap(p).Strategy = new SourcePropertyMappingStrategy(p));
        }

        PropertyMap<TSource, TTarget> AddPropertyMap(PropertyInfo propertyInfo)
        {
            var propertyMap = new PropertyMap<TSource, TTarget>(propertyInfo);
            _properties.Add(propertyMap);
            return propertyMap;
        }
    }
}