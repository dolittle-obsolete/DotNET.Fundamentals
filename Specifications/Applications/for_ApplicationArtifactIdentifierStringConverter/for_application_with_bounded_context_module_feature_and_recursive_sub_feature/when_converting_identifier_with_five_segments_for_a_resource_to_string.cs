/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Artifacts;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifierStringConverter.for_application_with_bounded_context_module_feature_and_recursive_sub_feature
{
    public class when_converting_identifier_with_five_segments_for_a_resource_to_string : given.an_application_resource_identifier_converter
    {
        const string bounded_context_name = "TheBoundedContext";
        const string module_name = "TheModule";
        const string feature_name = "TheFeature";
        const string sub_feature_name = "TheSubFeature";
        const string second_level_sub_feature_name = "TheSecondLevelSubFeature";
        const string artifact_name = "MyResource";
        const int artifact_generation = 1;

        static string expected =
            $"{application_name}{ApplicationArtifactIdentifierStringConverter.ApplicationSeparator}" +
            $"{bounded_context_name}{ApplicationArtifactIdentifierStringConverter.ApplicationLocationSeparator}" +
            $"{module_name}{ApplicationArtifactIdentifierStringConverter.ApplicationLocationSeparator}" +
            $"{feature_name}{ApplicationArtifactIdentifierStringConverter.ApplicationLocationSeparator}" +
            $"{sub_feature_name}{ApplicationArtifactIdentifierStringConverter.ApplicationLocationSeparator}" +
            $"{second_level_sub_feature_name}" +
            $"{ApplicationArtifactIdentifierStringConverter.ApplicationArtifactSeparator}{artifact_name}" +
            $"{ApplicationArtifactIdentifierStringConverter.ApplicationArtifactTypeSeparator}{artifact_type_name}" +
            $"{ApplicationArtifactIdentifierStringConverter.ApplicationArtifactGenerationSeperator}{artifact_generation}";

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

            var artifact = new Artifact(artifact_name, artifact_type.Object, artifact_generation);
            identifier = new ApplicationArtifactIdentifier(
                application.Object,
                location.Object,
                artifact);
        };

        Because of = () => result = converter.AsString(identifier);

        It should_return_expected_string = () => result.ShouldEqual(expected);
    }
}
