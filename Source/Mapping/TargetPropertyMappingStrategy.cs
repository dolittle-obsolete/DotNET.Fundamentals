// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;

namespace Dolittle.Mapping
{
    /// <summary>
    /// Represents an implementation of <see cref="IPropertyMappingStrategy"/> that supports mapping to
    /// a specified property.
    /// </summary>
    public class TargetPropertyMappingStrategy : IPropertyMappingStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TargetPropertyMappingStrategy"/> class.
        /// </summary>
        /// <param name="propertyInfo"><see cref="PropertyInfo"/> representing the property.</param>
        public TargetPropertyMappingStrategy(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
        }

        /// <summary>
        /// Gets the <see cref="PropertyInfo"/> representing the property.
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <inheritdoc/>
        public void Perform(IMappingTarget mappingTarget, object target, object sourceValue)
        {
            mappingTarget.SetValue(target, PropertyInfo, sourceValue);
        }
    }
}
