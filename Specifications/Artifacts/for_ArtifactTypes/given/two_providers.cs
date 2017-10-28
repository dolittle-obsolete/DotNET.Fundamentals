using System.Collections.Generic;
using Machine.Specifications;
using Moq;

namespace doLittle.Artifacts.for_ArtifactTypes.given
{
    public class two_providers : all_dependencies
    {
        protected static Mock<ICanProvideArtifactTypes> first_provider;
        protected static Mock<ICanProvideArtifactTypes> second_provider;

        protected static List<IArtifactType> first_provider_artifact_types;
        protected static List<IArtifactType> second_provider_artifact_types;

        Establish context = () =>
        {
            first_provider_artifact_types = new List<IArtifactType>();
            second_provider_artifact_types = new List<IArtifactType>();

            first_provider = new Mock<ICanProvideArtifactTypes>();
            first_provider.Setup(_ => _.Provide()).Returns(() => first_provider_artifact_types);
            second_provider = new Mock<ICanProvideArtifactTypes>();
            second_provider.Setup(_ => _.Provide()).Returns(() => second_provider_artifact_types);

            artifact_types_providers.Setup(_ => _.GetEnumerator()).Returns(() => new List<ICanProvideArtifactTypes>() {
                first_provider.Object,
                second_provider.Object
            }.GetEnumerator());
        };
    }
}