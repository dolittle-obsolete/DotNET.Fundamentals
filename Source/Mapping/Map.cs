/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Dolittle.Collections;
using Dolittle.Reflection;

namespace Dolittle.Mapping
{
    /// <summary>
    /// Represents a mapping description used by <see cref="IMapper"/>
    /// </summary>
    /// <typeparam name="TSource">Source type for the map</typeparam>
    /// <typeparam name="TTarget">Target type for the map</typeparam>
    public abstract class Map<TSource, TTarget> : IMap
    {
        List<PropertyMap<TSource, TTarget>> _properties = new List<PropertyMap<TSource, TTarget>>();

        /// <summary>
        /// Initializes a new instance of <see cref="Map{TSource, TTarget}"/>
        /// </summary>
        public Map()
        {
            AddDefaultPropertyMaps();
        }


        /// <summary>
        /// Describe a specific property
        /// </summary>
        /// <param name="property">Expression representing the property</param>
        /// <returns>A new map for the property</returns>
        protected PropertyMap<TSource, TTarget> Property(Expression<Func<TSource, object>> property)
        {
            var propertyInfo = property.GetPropertyInfo();
            return AddPropertyMap(propertyInfo);
        }

        /// <inheritdoc/>
        public IEnumerable<PropertyMap<TSource, TTarget>> Properties { get { return _properties; } }

        /// <inheritdoc/>
        public Type Source { get { return typeof(TSource); } }

        /// <inheritdoc/>
        public Type Target { get { return typeof(TTarget); } }

        IEnumerable<IPropertyMap> IMap.Properties { get { return _properties; } }

        void AddDefaultPropertyMaps()
        {
            typeof(TSource).GetTypeInfo().GetProperties().ForEach(p => AddPropertyMap(p).Strategy = new SourcePropertyMappingStrategy(p));
        }

        PropertyMap<TSource, TTarget> AddPropertyMap(System.Reflection.PropertyInfo propertyInfo)
        {
            var propertyMap = new PropertyMap<TSource, TTarget>(propertyInfo);
            _properties.Add(propertyMap);
            return propertyMap;
        }

    }
}