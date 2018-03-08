using System;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifierStringConverter
{
    public class when_converting_string_identifier_missing_application_area_in_string : given.an_application_resource_identifier_converter
    {
        const string bounded_context_name = "TheBoundedContext";
        static string string_identifier = 
            $"{application_name}{ApplicationArtifactIdentifierStringConverter.ApplicationSeparator}"+
            $"{bounded_context_name}{ApplicationArtifactIdentifierStringConverter.ApplicationLocationSeparator}" +
            $"{ApplicationArtifactIdentifierStringConverter.ApplicationArtifactSeparator}"+
            $"Resource"+
            $"{ApplicationArtifactIdentifierStringConverter.ApplicationArtifactTypeSeparator}"+
            $"{artifact_type}";
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => converter.FromString(string_identifier));

        It should_throw_missing_application_locations = () => exception.ShouldBeOfExactType<MissingApplicationArea>();
    }
}
