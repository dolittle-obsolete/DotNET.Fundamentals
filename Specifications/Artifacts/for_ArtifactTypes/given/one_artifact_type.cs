using System.Collections.Generic;
using Machine.Specifications;
using Moq;

namespace Dolittle.Artifacts.for_ArtifactTypes.given
{
    public class one_artifact_type : all_dependencies
    {
        protected const string identifier = "Fourty Two";
        protected static Mock<IArtifactType> artifact_type;

        Establish context = () =>
        {
            artifact_type = new Mock<IArtifactType>();
            artifact_type.SetupGet(_ => _.Identifier).Returns(identifier);

            artifact_type_instances.Setup(_ => _.GetEnumerator()).Returns(() => new List<IArtifactType>() {
                artifact_type.Object,
            }.GetEnumerator());
        };
    }
}