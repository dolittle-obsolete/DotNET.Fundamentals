using Dolittle.Artifacts;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifierStringConverter.given
{
    public class an_application_resource_identifier_converter : all_dependencies
    {
        protected const string application_name = "MyApplication";
        protected const string artifact_type_name = "TheResourceType";

        protected static ApplicationArtifactIdentifierStringConverter converter;

        protected static Mock<IArtifactType> artifact_type;
        protected static Mock<IApplicationStructure> application_structure;

        Establish context = () =>
        {
            var subFeatureFragment = new Mock<IApplicationStructureFragment>();
            subFeatureFragment.SetupGet(_ => _.Required).Returns(false);
            subFeatureFragment.SetupGet(_ => _.Recursive).Returns(true);
            subFeatureFragment.SetupGet(_ => _.Type).Returns(typeof(SubFeature));
            
            var featureFragment = new Mock<IApplicationStructureFragment>();
            featureFragment.SetupGet(_ => _.Required).Returns(false);
            featureFragment.SetupGet(_ => _.Recursive).Returns(false);
            featureFragment.SetupGet(_ => _.Type).Returns(typeof(Feature));
            featureFragment.SetupGet(_ => _.Children).Returns(new IApplicationStructureFragment[] { subFeatureFragment.Object });
            
            var moduleFragment = new Mock<IApplicationStructureFragment>();
            moduleFragment.SetupGet(_ => _.Required).Returns(false);
            moduleFragment.SetupGet(_ => _.Recursive).Returns(false);
            moduleFragment.SetupGet(_ => _.Type).Returns(typeof(Module));
            moduleFragment.SetupGet(_ => _.Children).Returns(new IApplicationStructureFragment[] { featureFragment.Object });


            var boundedContextFragment = new Mock<IApplicationStructureFragment>();
            boundedContextFragment.SetupGet(_ => _.Required).Returns(true);
            boundedContextFragment.SetupGet(_ => _.Recursive).Returns(false);
            boundedContextFragment.SetupGet(_ => _.Type).Returns(typeof(BoundedContext));
            boundedContextFragment.SetupGet(_ => _.Children).Returns(new IApplicationStructureFragment[] { moduleFragment.Object });

            application_structure = new Mock<IApplicationStructure>();
            application_structure.SetupGet(_ => _.Root).Returns(boundedContextFragment.Object);

            application.SetupGet(a => a.Name).Returns(application_name);
            application.SetupGet(a => a.Structure).Returns(application_structure.Object);

            artifact_type = new Mock<IArtifactType>();
            artifact_type.SetupGet(a => a.Identifier).Returns(artifact_type_name);

            artifact_types.Setup(a => a.GetFor(artifact_type_name)).Returns(artifact_type.Object);

            converter = new ApplicationArtifactIdentifierStringConverter(application.Object, artifact_types.Object);
        };
    }
}
