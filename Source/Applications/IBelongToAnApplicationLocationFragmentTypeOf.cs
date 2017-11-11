/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Applications
{
    /// <summary>
    /// Defines a parent relationship to another <see cref="IApplicationLocationFragment"/> of a specific
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IApplicationLocationFragment"/></typeparam>
    public interface IBelongToAnApplicationLocationFragmentTypeOf<T>
        where T : IApplicationLocationFragment
    {
        /// <summary>
        /// Gets the parent <see cref="IApplicationLocationFragment">location</see>
        /// </summary>
        T Parent { get; }
    }
}