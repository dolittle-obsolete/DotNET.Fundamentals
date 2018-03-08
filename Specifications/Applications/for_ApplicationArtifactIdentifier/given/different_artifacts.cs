/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Artifacts;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifier.given
{
    public class different_artifacts
    {
        protected static ApplicationArtifactIdentifier identifier_a;
        protected static ApplicationArtifactIdentifier identifier_b;

        Establish context = () =>
        {
            var application = new Mock<IApplication>();
            application.SetupGet(a => a.Name).Returns("SomeApplication");
            var area = (ApplicationArea)"Some Area";
            var location = Mock.Of<IApplicationLocation>();
            var artifactA = new Mock<IArtifact>();
            artifactA.SetupGet(_ => _.Name).Returns("ArtifactA");
            var artifactB = new Mock<IArtifact>();
            artifactB.SetupGet(_ => _.Name).Returns("ArtifactB");

            identifier_a = new ApplicationArtifactIdentifier(application.Object, area, location, artifactA.Object);
            identifier_b = new ApplicationArtifactIdentifier(application.Object, area, location, artifactB.Object);
        };
    }
}
