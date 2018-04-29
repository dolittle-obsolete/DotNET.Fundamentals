/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Represents an implementation of <see cref="IPropertyDescriptionBuilder"/>
    /// </summary>
    public class PropertyDescriptionBuilder : IPropertyDescriptionBuilder
    {
        readonly PropertyInfo _property;
        readonly string _name;
        readonly object _defaultValue;
        readonly int _number;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="number"></param>
        public PropertyDescriptionBuilder(PropertyInfo property, string name, object defaultValue, int number)
        {
            _property = property;
            _name = name;
            _defaultValue = defaultValue;
            _number = number;
        }

        /// <summary>
        /// Specify a specific name of the property
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <returns>A new <see cref="PropertyDescriptionBuilder"/> for the chain</returns>
        public PropertyDescriptionBuilder WithName(string name) => new PropertyDescriptionBuilder(_property, name, _defaultValue, _number);

        /// <summary>
        /// Specify a specific default value of the property when a value is not specified
        /// </summary>
        /// <param name="defaultValue">Default value of the property</param>
        /// <returns>A new <see cref="PropertyDescriptionBuilder"/> for the chain</returns>
        public PropertyDescriptionBuilder WithDefaultValue(object defaultValue) => new PropertyDescriptionBuilder(_property, _name, defaultValue, _number);

        /// <summary>
        /// Specify a number representing the property
        /// </summary>
        /// <param name="number">Number for property</param>
        /// <returns>A new <see cref="PropertyDescriptionBuilder"/> for the chain</returns>
        public PropertyDescriptionBuilder WithNumber(int number) => new PropertyDescriptionBuilder(_property, _name, _defaultValue, number);

        /// <inheritdoc/>
        public PropertyDescription Build()
        {
            var propertyDescription = new PropertyDescription(_property, _name, _defaultValue, _number);
            return propertyDescription;
        }
    }
}