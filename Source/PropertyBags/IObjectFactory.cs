/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.PropertyBags
{
    using System;
    /// <summary>
    /// Defines an interface for building types from a <see cref="PropertyBag" />
    /// </summary>
    public interface IObjectFactory
    {
        /// <summary>
        /// Builds the type using the <see cref="PropertyBag" /> as the source to populating it.
        /// </summary>
        /// <param name="typeToBuild">The type to build</param>
        /// <param name="source">The source for populating it.</param>
        /// <returns>An instance of the specified type populated with values from the <see cref="PropertyBag" /> as an object</returns>
        object Build(Type typeToBuild, PropertyBag source);
        
        /// <summary>
        /// Builds the type using the <see cref="PropertyBag" /> as the source to populating it.
        /// </summary>
        /// <param name="source">The source for populating it.</param>
        /// <typeparam name="T">The type to build</typeparam>
        /// <returns>An instance of the specified type populated with values from the <see cref="PropertyBag" /></returns>
        T Build<T>(PropertyBag source);
    }
}