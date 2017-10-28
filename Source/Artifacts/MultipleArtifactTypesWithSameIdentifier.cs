/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace doLittle.Artifacts
{
    /// <summary>
    /// The exception that gets thrown when there are multiple <see cref="IArtifactType"/> with same identifier
    /// </summary>
    public class MultipleArtifactTypesWithSameIdentifier : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MultipleArtifactTypesWithSameIdentifier"/>
        /// </summary>
        /// <param name="artifactTypes"><see cref="IArtifactType">Artifact types</see> that there are multiple of</param>
        public MultipleArtifactTypesWithSameIdentifier(IEnumerable<IArtifactType> artifactTypes)
            : base($"Identifier '{artifactTypes.First().Identifier}' is used multiple times as a well known artifact type. The types defining them are: {string.Join(", ", artifactTypes.Select(_ => _.GetType().AssemblyQualifiedName))}")
        { }
    }
}