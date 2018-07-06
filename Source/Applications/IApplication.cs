/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Defines an application 
    /// </summary>
    public interface IApplication : IEquatable<IApplication>, IComparable, IComparable<IApplication>
    {
        /// <summary>
        /// Gets the <see cref="ApplicationName">name</see> of the <see cref="IApplication"/>
        /// </summary>
        ApplicationName Name { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationStructure">structure</see> of the <see cref="IApplication"/>
        /// </summary>
        IApplicationStructure Structure { get; }

        /// <summary>
        /// Gets any <see cref="IApplicationLocationSegment">segments</see> that act as prefixes
        /// </summary>
        IEnumerable<IApplicationLocationSegment> Prefixes { get; }
    }
}
