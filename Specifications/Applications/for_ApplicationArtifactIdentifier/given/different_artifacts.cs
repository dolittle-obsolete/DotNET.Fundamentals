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
            var location = Mock.Of<IApplicationLocation>(_ => _.Equals(
                Moq.It.IsAny<IApplicationLocation>()) == true
                );
            var artifactA = Mock.Of<IArtifact>();

            var artifactB = Mock.Of<IArtifact>();

            identifier_a = new ApplicationArtifactIdentifier(application.Object, area, location, artifactA);
            identifier_b = new ApplicationArtifactIdentifier(application.Object, area, location, artifactB);
        };
    }
}
