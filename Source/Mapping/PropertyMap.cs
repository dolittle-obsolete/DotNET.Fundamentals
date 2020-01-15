// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;

namespace Dolittle.Mapping
{
    /// <summary>
    /// Represents a mapping for a specific property on a type.
    /// </summary>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TTarget">Target type.</typeparam>
    public class PropertyMap<TSource, TTarget> : IPropertyMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMap{TSource, TTarget}"/> class.
        /// </summary>
        /// <param name="from"><see cref="PropertyInfo"/> to map from.</param>
        public PropertyMap(PropertyInfo from)
        {
            From = from;
        }

        /// <inheritdoc/>
        public PropertyInfo From { get; }

        /// <inheritdoc/>
        public IPropertyMappingStrategy Strategy { get; set; }
    }
}
