// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Dolittle.Mapping
{
    /// <summary>
    /// Defines a map that describes mapping for an object.
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface will be automatically registered.
    /// You most likely want to subclass <see cref="Map{TSource,TTarget}"/>.
    /// </remarks>
    public interface IMap
    {
        /// <summary>
        /// Gets the source type the map is for.
        /// </summary>
        Type Source { get; }

        /// <summary>
        /// Gets the target type the map is for.
        /// </summary>
        Type Target { get; }

        /// <summary>
        /// Gets get the mapped properties.
        /// </summary>
        IEnumerable<IPropertyMap> Properties { get; }
    }
}
