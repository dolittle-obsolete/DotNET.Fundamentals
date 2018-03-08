using System;
using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Artifacts.for_ArtifactTypes
{
    public class when_initialized_with_providers_providing_duplicate_types : given.two_providers
    {
        const string identifier = "FourtyTwo";

        static Mock<IArtifactType> first_provider_artifact_type;
        static Mock<IArtifactType> second_provider_artifact_type;

        static Exception exception;

        Establish context = () => 
        {
            first_provider_artifact_type = new Mock<IArtifactType>();
            first_provider_artifact_type.SetupGet(_ => _.Identifier).Returns(identifier);

            first_provider_artifact_types.Add(first_provider_artifact_type.Object);

            second_provider_artifact_type = new Mock<IArtifactType>();
            second_provider_artifact_type.SetupGet(_ => _.Identifier).Returns(identifier);
            second_provider_artifact_types.Add(second_provider_artifact_type.Object);
        };

        Because of = () => exception = Catch.Exception(() => new ArtifactTypes(artifact_types_providers.Object));

        It should_throw_ambiguous_artifact_type_definitions = () => exception.ShouldBeOfExactType<MultipleArtifactTypesWithSameIdentifier>();
    }
}