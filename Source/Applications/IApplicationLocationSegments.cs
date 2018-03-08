/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Dolittle.Applications
{
    /// <summary>
    /// Defines a system for working with <see cref="IApplicationLocationSegment">Application location segments</see>
    /// </summary>
    public interface IApplicationLocationSegments
    {
        /// <summary>
        /// Validate if given <see cref="Type"/> of an <see cref="IApplicationLocationSegment"/> is as expected
        /// </summary>
        /// <param name="type"><see cref="Type"/> to validate</param>
        /// <remarks>
        /// A specific type of <see cref="IApplicationLocationSegment"/> *MUST* have one constructor.
        /// This constructor should have one of the following signatures:
        /// 
        /// .ctor(string name)
        /// .ctor(IApplicationLocationSegmentName name)
        /// .ctor(IApplicationLocationSegment parent, string name)
        /// .ctor(IApplicationLocationSegment parent, IApplicationLocationSegmentName name)
        /// 
        /// The parameter type representing the <see cref="IApplicationLocationSegmentName"/> should  have a
        /// constructor with one of the following signatures:
        /// 
        /// .ctor()
        /// .ctor(string name)
        /// 
        /// If it only has a default constructor, it *MUST* implement a <see cref="ConceptAs{T}"/> in order for
        /// the system to be able to actually set the name
        /// </remarks>
        void Validate(Type type);

        /// <summary>
        /// Create an instance of the given <see cref="IApplicationLocationSegment"/> <see cref="Type"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> of <see cref="IApplicationLocationSegment"/> to create instance of</param>
        /// <param name="name"><see cref="String">Name</see> of the segment</param>
        /// <param name="parent">Optional <see cref="IApplicationLocationSegment">parent</see></param>
        /// <returns></returns>
        IApplicationLocationSegment Create(Type type, string name, IApplicationLocationSegment parent=null);
    }
}