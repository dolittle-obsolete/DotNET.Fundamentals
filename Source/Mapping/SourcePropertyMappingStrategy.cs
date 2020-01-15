// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;

namespace Dolittle.Mapping
{
    /// <summary>
    /// Represents a <see cref="IPropertyMappingStrategy"/> that is typically used when property should
    /// map to self - same property as source.
    /// </summary>
    /// <remarks>
    /// If the property does not exist in the target, it will just ignore it and the value won't be set.
    /// It does not qualify to be an exceptional state.
    /// </remarks>
    public class SourcePropertyMappingStrategy : IPropertyMappingStrategy
    {
        readonly PropertyInfo _propertyInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourcePropertyMappingStrategy"/> class.
        /// </summary>
        /// <param name="propertyInfo"><see cref="PropertyInfo"/> to base it from.</param>
        public SourcePropertyMappingStrategy(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        /// <inheritdoc/>
        public void Perform(IMappingTarget mappingTarget, object target, object value)
        {
        }
    }
}
