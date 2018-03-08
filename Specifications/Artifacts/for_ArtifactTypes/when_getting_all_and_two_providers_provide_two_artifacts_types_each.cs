using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Artifacts.for_ArtifactTypes
{
    public class when_getting_all_and_two_providers_provide_two_artifacts_types_each : given.two_providers
    {
        static Mock<IArtifactType> first_provider_first_artifact_type;
        static Mock<IArtifactType> first_provider_second_artifact_type;
        static Mock<IArtifactType> second_provider_first_artifact_type;
        static Mock<IArtifactType> second_provider_second_artifact_type;

        static IEnumerable<IArtifactType> result;

        static ArtifactTypes artifact_types;

        Establish context = () => 
        {
            first_provider_first_artifact_type = new Mock<IArtifactType>();
            first_provider_first_artifact_type.SetupGet(_ => _.Identifier).Returns("FirstProviderFirstArtifact");
            first_provider_artifact_types.Add(first_provider_first_artifact_type.Object);
            first_provider_second_artifact_type = new Mock<IArtifactType>();
            first_provider_second_artifact_type.SetupGet(_ => _.Identifier).Returns("FirstProviderSecondArtifact");
            first_provider_artifact_types.Add(first_provider_second_artifact_type.Object);

            second_provider_first_artifact_type = new Mock<IArtifactType>();
            second_provider_first_artifact_type.SetupGet(_ => _.Identifier).Returns("SecondProviderFirstArtifact");
            second_provider_artifact_types.Add(second_provider_first_artifact_type.Object);
            second_provider_second_artifact_type = new Mock<IArtifactType>();
            second_provider_second_artifact_type.SetupGet(_ => _.Identifier).Returns("SecondProviderSecondArtifact");
            second_provider_artifact_types.Add(second_provider_second_artifact_type.Object);

            artifact_types = new ArtifactTypes(artifact_types_providers.Object);
        };

        Because of = () => result = artifact_types.All;

        It should_hold_all_four_artifact_types = () => result.ShouldContainOnly(
                                                            first_provider_first_artifact_type.Object,
                                                            first_provider_second_artifact_type.Object,
                                                            second_provider_first_artifact_type.Object,
                                                            second_provider_second_artifact_type.Object);
    }
}