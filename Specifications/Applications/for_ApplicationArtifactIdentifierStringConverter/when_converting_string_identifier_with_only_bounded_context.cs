using System.Linq;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifierStringConverter
{
    public class when_converting_string_identifier_with_only_bounded_context : given.an_application_resource_identifier_converter
    {
        const string bounded_context_name = "TheBoundedContext";
        const string module_name = "TheModule";
        const string feature_name = "TheFeature";
        const string sub_feature_name = "TheSubFeature";
        const string second_level_sub_feature_name = "TheSecondLevelSubFeature";
        const string resource_name = "MyResource";
        const string area_name = "MyArea";

        static string string_identifier =
            $"{application_name}{ApplicationArtifactIdentifierStringConverter.ApplicationSeparator}" +
            $"{bounded_context_name}{ApplicationArtifactIdentifierStringConverter.ApplicationLocationSeparator}" +
            $"{ApplicationArtifactIdentifierStringConverter.ApplicationArtifactSeparator}{resource_name}"+
            $"{ApplicationArtifactIdentifierStringConverter.ApplicationArtifactTypeSeparator}{artifact_type_name}"+
            $"{ApplicationArtifactIdentifierStringConverter.ApplicationAreaSeperator}{area_name}";

        static IApplicationArtifactIdentifier identifier;

        Because of = () => identifier = converter.FromString(string_identifier);

        It should_return_a_matching_identifier = () => identifier.ShouldNotBeNull();
        It should_hold_the_application = () => identifier.Application.ShouldEqual(application.Object);
        It should_hold_the_artifact = () => identifier.Artifact.Name.Value.ShouldEqual(resource_name);
        It should_hold_the_bounded_context_segment = () => identifier.Location.Segments.First().Name.AsString().ShouldEqual(bounded_context_name);
        It should_hold_the_artifact_type = () => identifier.Artifact.Type.ShouldEqual(artifact_type.Object);
        It should_hold_the_area = () => identifier.Area.Value.ShouldEqual(area_name);
    }
}
