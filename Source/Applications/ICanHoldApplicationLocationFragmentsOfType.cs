/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace doLittle.Applications
{
    /// <summary>
    /// Defines something that can hold <see cref="IApplicationLocationFragment">application location fragments</see>
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IApplicationLocationFragment"/></typeparam>
    public interface ICanHoldApplicationLocationFragmentsOfType<T>
        where T:IApplicationLocationFragment
    {
        /// <summary>
        /// Gets the children <see cref="IEnumerable{IApplicationLocationFragment}">location fragments</see>
        /// </summary>
        IEnumerable<T> Children { get; }
    }
}
