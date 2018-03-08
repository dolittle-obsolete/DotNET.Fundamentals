using Dolittle.Artifacts;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifierStringConverter
{
    public class when_converting_identifier_with_five_segments_for_a_resource_to_string : given.an_application_resource_identifier_converter
    {
        const string area_name = "TheArea";
        const string bounded_context_name = "TheBoundedContext";
        const string module_name = "TheModule";
        const string feature_name = "TheFeature";
        const string sub_feature_name = "TheSubFeature";
        const string second_level_sub_feature_name = "TheSecondLevelSubFeature";
        const string artifact_name = "MyResource";

        static string expected =
            $"{application_name}{ApplicationArtifactIdentifierStringConverter.ApplicationSeparator}" +
            $"{bounded_context_name}{ApplicationArtifactIdentifierStringConverter.ApplicationLocationSeparator}" +
            $"{module_name}{ApplicationArtifactIdentifierStringConverter.ApplicationLocationSeparator}" +
            $"{feature_name}{ApplicationArtifactIdentifierStringConverter.ApplicationLocationSeparator}" +
            $"{sub_feature_name}{ApplicationArtifactIdentifierStringConverter.ApplicationLocationSeparator}" +
            $"{second_level_sub_feature_name}" +
            $"{ApplicationArtifactIdentifierStringConverter.ApplicationArtifactSeparator}{artifact_name}" +
            $"{ApplicationArtifactIdentifierStringConverter.ApplicationArtifactTypeSeparator}{artifact_type_name}" +
            $"{ApplicationArtifactIdentifierStringConverter.ApplicationAreaSeperator}{area_name}";

        static IApplicationArtifactIdentifier identifier;

        static string result;

        Establish context = () =>
        {
            var application = new Mock<IApplication>();
            application.SetupGet(a => a.Name).Returns(application_name);
            var bounded_context = new BoundedContext(bounded_context_name);
            var module = new Module(bounded_context, module_name);
            var feature = new Feature(module, feature_name);
            var sub_feature = new SubFeature(feature, sub_feature_name);
            var second_level_sub_feature = new SubFeature(sub_feature, second_level_sub_feature_name);

            var location = new Mock<IApplicationLocation>();
            location.SetupGet(_ => _.Segments).Returns(new IApplicationLocationSegment[] {
                bounded_context,
                module,
                feature,
                sub_feature,
                second_level_sub_feature
            });

            var artifact = new Artifact(artifact_name, artifact_type.Object);
            identifier = new ApplicationArtifactIdentifier(
                application.Object,
                area_name,
                location.Object,
                artifact);
        };

        Because of = () => result = converter.AsString(identifier);

        It should_return_expected_string = () => result.ShouldEqual(expected);
    }
}
