/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using doLittle.Artifacts;

namespace doLittle.Applications
{
    /// <summary>
    /// Defines an identifier for <see cref="IArtifact">resources</see> in an <see cref="IApplication"/>
    /// </summary>
    public interface IApplicationArtifactIdentifier : IEquatable<IApplicationArtifactIdentifier>, IComparable, IComparable<IApplicationArtifactIdentifier>
    {
        /// <summary>
        /// Gets the <see cref="IApplication"/> the resource belongs to
        /// </summary>
        IApplication Application { get; }

        /// <summary>
        /// Gets the <see cref="ApplicationArea"/> the resource belongs to
        /// </summary>
        /// <returns></returns>
        ApplicationArea Area { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationLocation">location</see>
        /// </summary>
        IApplicationLocation Location { get; }

        /// <summary>
        /// Gets the <see cref="IArtifact">artifact</see>
        /// </summary>
        IArtifact Artifact { get; }
    }
}