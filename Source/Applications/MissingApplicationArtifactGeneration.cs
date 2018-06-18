/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Runtime.Serialization;

namespace Dolittle.Applications
{
    internal class MissingApplicationArtifactGeneration : Exception
    {
        public MissingApplicationArtifactGeneration(string identifierString) 
        : base($"Missing application artifact generation in '{identifierString}'. Expected format : {ApplicationArtifactIdentifierStringConverter.ExpectedFormat}")
        {
        }

    }
}