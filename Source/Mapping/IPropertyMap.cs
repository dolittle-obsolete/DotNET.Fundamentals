// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;

namespace Dolittle.Mapping
{
    /// <summary>
    /// Defines a map for a property on an object.
    /// </summary>
    public interface IPropertyMap
    {
        /// <summary>
        /// Gets the <see cref="PropertyInfo">property</see> in which we are mapping from.
        /// </summary>
        PropertyInfo From { get; }

        /// <summary>
        /// Gets or sets the <see cref="IPropertyMappingStrategy">strategy</see> to use.
        /// </summary>
        IPropertyMappingStrategy Strategy { get; set; }
    }
}
