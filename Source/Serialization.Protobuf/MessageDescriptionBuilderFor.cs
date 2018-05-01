/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Dolittle.Reflection;

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Represents a builder for building <see cref="MessageDescription"/> for a specified type
    /// </summary>
    public class MessageDescriptionBuilderFor<T> : IMessageDescriptionBuilderFor<T>
    {
        IEnumerable<IPropertyDescriptionBuilder> _propertyDescriptionBuilders;
        string _name;

        /// <summary>
        /// Initializes a new instance of <see cref="MessageDescriptionBuilderFor{T}"/>
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="propertyDescriptionBuilders"><see cref="IPropertyDescriptionBuilder">Property builders</see></param>
        public MessageDescriptionBuilderFor(string name, IEnumerable<IPropertyDescriptionBuilder> propertyDescriptionBuilders = null)
        {
            _propertyDescriptionBuilders = propertyDescriptionBuilders ?? new IPropertyDescriptionBuilder[0];
            _name = name;
        }

        /// <inheritdoc/>
        public MessageDescription Build()
        {
            var properties = _propertyDescriptionBuilders.Select(_ => _.Build()).ToArray();
            var messageDescription = new MessageDescription(typeof(T), properties, _name);
            return messageDescription;
        }

        /// <inheritdoc/>
        public IMessageDescriptionBuilderFor<T> Property<TProp>(Expression<Func<TProp>> property, Func<IPropertyDescriptionBuilder, IPropertyDescriptionBuilder> propertyDescriptionBuilderCallback)
        {
            var propertyInfo = property.GetPropertyInfo();
            IPropertyDescriptionBuilder propertyDescriptionBuilder = new PropertyDescriptionBuilder(propertyInfo, propertyInfo.Name, null, 0);
            propertyDescriptionBuilder = propertyDescriptionBuilderCallback(propertyDescriptionBuilder);
            var propertyDescriptionBuilders = new List<IPropertyDescriptionBuilder>(_propertyDescriptionBuilders);
            propertyDescriptionBuilders.Add(propertyDescriptionBuilder);
            var messageDescriptionBuilder = new MessageDescriptionBuilderFor<T>(_name, propertyDescriptionBuilders);
            return messageDescriptionBuilder;
        }

        /// <inheritdoc/>
        public IMessageDescriptionBuilderFor<T> WithAllProperties()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public|BindingFlags.Instance);
            var propertyDescriptionBuilders = properties.Select(_ => new PropertyDescriptionBuilder(_, _.Name, null, 0)).ToArray();
            var messageDescriptionBuilder = new MessageDescriptionBuilderFor<T>(_name, propertyDescriptionBuilders);
            return messageDescriptionBuilder;
        }
    }
}