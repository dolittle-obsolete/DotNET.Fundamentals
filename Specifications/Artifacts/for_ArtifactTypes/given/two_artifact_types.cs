using System.Collections.Generic;
using Machine.Specifications;
using Moq;

namespace Dolittle.Artifacts.for_ArtifactTypes.given
{
    public class two_artifact_types : all_dependencies
    {
        protected const string first_artifact_type_identifier = "First";
        protected const string second_artifact_type_identifier = "Second";
        protected static Mock<IArtifactType> first_artifact_type;
        protected static Mock<IArtifactType> second_artifact_type;

        Establish context = () =>
        {
            first_artifact_type = new Mock<IArtifactType>();
            first_artifact_type.SetupGet(_ => _.Identifier).Returns(first_artifact_type_identifier);
            second_artifact_type = new Mock<IArtifactType>();
            second_artifact_type.SetupGet(_ => _.Identifier).Returns(second_artifact_type_identifier);

            artifact_type_instances.Setup(_ => _.GetEnumerator()).Returns(() => new List<IArtifactType>() {
                first_artifact_type.Object,
                second_artifact_type.Object
            }.GetEnumerator());
        };
    }
}