/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Artifacts;

namespace Dolittle.Applications
{
    /// <summary>
    /// The exception that gets thrown when parsing an <see cref="IApplicationArtifactIdentifier"/> that has an invalid format
    /// </summary>
    public class InvalidApplicationArtifactIdentifierFormat : ArgumentException
    {
        /// <summary>
        /// Initializes a new 
        /// </summary>
        public InvalidApplicationArtifactIdentifierFormat(string identifierString)
            : base($"Invalid format for '{identifierString}'. Expected format : {ApplicationArtifactIdentifierStringConverter.ExpectedFormat}")
        { }
    }
}
