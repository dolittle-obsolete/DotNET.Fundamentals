/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;
using Dolittle.Artifacts;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when parsing an <see cref="IApplicationArtifactIdentifier"/>
    /// from a string and its not possible to find the <see cref="IArtifactGeneration"/> in the <see cref="string"/>
    /// </summary>
    public class MissingApplicationArtifactGeneration : ArgumentException
    {
        /// <summary>
        /// Initializes the <see cref="MissingApplicationArtifactGeneration"> exception
        /// </summary>
        /// <param name="identifierString"></param>
        public MissingApplicationArtifactGeneration(string identifierString) 
        : base($"Missing application artifact generation in '{identifierString}'. Expected format : {ApplicationArtifactIdentifierStringConverter.ExpectedFormat}")
        {
        }

    }
}