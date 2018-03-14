/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Collections;
using Dolittle.Types;

namespace Dolittle.Artifacts
{
    /// <summary>
    /// Represents an implemenation of <see cref="IArtifactTypes"/>
    /// </summary>
    public class ArtifactTypes : IArtifactTypes
    {   
        readonly IEnumerable<IArtifactType> _all;
        Dictionary<string, IArtifactType> _artifactTypesByIdentifier = new Dictionary<string, IArtifactType>();

        /// <summary>
        /// Initializes a new instance of <see cref="ArtifactTypes"/>
        /// </summary>
        /// <param name="artifactTypes"><see cref="IInstancesOf{IArtifactTypes}">Instances</see> of <see cref="IArtifactTypes"/></param>
        public ArtifactTypes(IInstancesOf<IArtifactType> artifactTypes)
        {
            _all = artifactTypes.ToArray();
            ThrowIfMultipleArtifactTypesWithSameIdentifier();

            _artifactTypesByIdentifier = _all.ToDictionary(artifactType => artifactType.Identifier, artifactType => artifactType);
        }

        /// <inheritdoc/>
        public IEnumerable<IArtifactType> All => _all;

        /// <inheritdoc/>
        public bool Exists(string identifier)
        {
            return _artifactTypesByIdentifier.ContainsKey(identifier);
        }

        /// <inheritdoc/>
        public IArtifactType GetFor(string identifier)
        {
            return _artifactTypesByIdentifier[identifier];
        }

        void ThrowIfMultipleArtifactTypesWithSameIdentifier()
        {
            var grouped = _all.GroupBy(artifactType => artifactType.Identifier);
            grouped.ForEach(group => {
                if( group.Count() > 1 ) throw new MultipleArtifactTypesWithSameIdentifier(group);
            });
        }
    }
}