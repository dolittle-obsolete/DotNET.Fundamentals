/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Linq.Expressions;
using Dolittle.Reflection;

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageDescriptionBuilderFor<T> : IMessageDescriptionBuilderFor<T>
    {
        /// <inheritdoc/>
        public MessageDescription Build()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IMessageDescriptionBuilderFor<T> Property<TProp>(Expression<Func<TProp>> property, Func<IPropertyDescriptionBuilder, IPropertyDescriptionBuilder> propertyDescriptionBuilderCallback)
        {
            var propertyInfo = property.GetPropertyInfo();
            throw new NotImplementedException();
        }
    }
}