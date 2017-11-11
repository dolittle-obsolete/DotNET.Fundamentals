/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Applications
{
    /// <summary>
    /// Defines a location within an application
    /// </summary>
    public interface IApplicationLocationFragment
    {
        /// <summary>
        /// Gets the <see cref="IApplicationLocationFragmentName">application location name</see>
        /// </summary>
        IApplicationLocationFragmentName    Name { get; }
    }

    /// <summary>
    /// Defines a location within the application
    /// </summary>
    public interface IApplicationLocationFragment<TName> : IApplicationLocationFragment
        where TName: IApplicationLocationFragmentName
    {
        /// <summary>
        /// Gets the <see cref="IApplicationLocationFragmentName">name</see> of the location
        /// </summary>
        new TName Name { get; }
    }
}
