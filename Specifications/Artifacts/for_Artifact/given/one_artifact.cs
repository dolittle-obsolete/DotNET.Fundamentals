/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Moq;

namespace Dolittle.Artifacts.Specs.for_Artifact.given
{
    public class one_artifact
    {
        protected static Artifact artifact;

        Establish context = () => 
        {
            var type = new Mock<IArtifactType>();
            type.SetupGet(_ => _.Identifier).Returns("ArtifactType");
            artifact = new Artifact("Artifact", type.Object, 1);
        };
    }
}