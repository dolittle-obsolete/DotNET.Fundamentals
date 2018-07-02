/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Moq;

namespace Dolittle.Artifacts.Specs.for_Artifact.given
{
    public class artifacts_with_different_ArtifactType : two_artifacts
    {
        Establish context = () =>
        {
            var artifactName = "Artifact";
            var typeA = new Mock<IArtifactType>();
            var typeB = new Mock<IArtifactType>();

            typeA.SetupGet(_ => _.Identifier).Returns("ArtifactTypeA");
            typeB.SetupGet(_ => _.Identifier).Returns("ArtifactTypeB");

            var generation = 1;

            artifactA = new Artifact(artifactName, typeA.Object, generation);
            artifactB = new Artifact(artifactName, typeB.Object, generation);
        };
    }
}