/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Defines the type of an <see cref="Artifact"/>
    /// </summary>
    public interface IArtifactType
    {
        /// <summary>
        /// Gets the identifier of the type
        /// </summary>
        string Identifier { get; }
    }
}