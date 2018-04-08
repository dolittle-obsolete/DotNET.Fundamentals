/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Applications
{
    /// <summary>
    /// Defines a location within an application
    /// </summary>
    public interface IApplicationLocationSegment
    {
        /// <summary>
        /// Gets the <see cref="IApplicationLocationSegmentName">application location name</see>
        /// </summary>
        IApplicationLocationSegmentName    Name { get; }

        /// <summary>
        /// Add a <see cref="IApplicationLocationSegment">child</see>
        /// </summary>
        /// <param name="child"><see cref="IApplicationLocationSegment">child</see> to add</param>
        void AddChild(IApplicationLocationSegment child);

    }

    /// <summary>
    /// Defines a location within the application
    /// </summary>
    public interface IApplicationLocationSegment<TName> : IApplicationLocationSegment
        where TName: IApplicationLocationSegmentName
    {
        /// <summary>
        /// Gets the <see cref="IApplicationLocationSegmentName">name</see> of the location
        /// </summary>
        new TName Name { get; }
    }
}
