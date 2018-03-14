using System;
using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Artifacts.for_ArtifactTypes
{
    public class when_initialized_with_duplicate_artifact_types : given.no_artifact_types
    {
        protected const string identifier = "First";
        protected static Mock<IArtifactType> first_artifact_type;
        protected static Mock<IArtifactType> second_artifact_type;
        protected static Exception exception;

        Establish context = () =>
        {
            first_artifact_type = new Mock<IArtifactType>();
            first_artifact_type.SetupGet(_ => _.Identifier).Returns(identifier);
            second_artifact_type = new Mock<IArtifactType>();
            second_artifact_type.SetupGet(_ => _.Identifier).Returns(identifier);

            artifact_type_instances.Setup(_ => _.GetEnumerator()).Returns(() => new List<IArtifactType>() {
                first_artifact_type.Object,
                second_artifact_type.Object
            }.GetEnumerator());
        };

        Because of = () => exception = Catch.Exception(() => new ArtifactTypes(artifact_type_instances.Object));

        It should_throw_ambiguous_artifact_type_definitions = () => exception.ShouldBeOfExactType<MultipleArtifactTypesWithSameIdentifier>();
    }
}