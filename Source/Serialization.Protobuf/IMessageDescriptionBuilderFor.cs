/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq.Expressions;

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Defines a builder for building <see cref="MessageDescription"/>
    /// </summary>
    public interface IMessageDescriptionBuilderFor<T>
    {
        /// <summary>
        /// Start building a <see cref="PropertyDescription">description</see> for a property
        /// </summary>
        /// <param name="property"><see cref="Expression"/> representing a reference to the property to build for</param>
        /// <param name="propertyDescriptionBuilderCallback">Callback that gets called with the <see cref="IPropertyDescriptionBuilder"/></param>
        /// <returns>A continuation of the <see cref="IMessageDescriptionBuilderFor{T}"/></returns>
        IMessageDescriptionBuilderFor<T> Property<TProp>(Expression<Func<TProp>> property, Func<IPropertyDescriptionBuilder, IPropertyDescriptionBuilder> propertyDescriptionBuilderCallback);

        /// <summary>
        /// Builds a completed <see cref="MessageDescription"/>
        /// </summary>
        /// <returns><see cref="MessageDescription"/></returns>
        MessageDescription Build();
    }
}