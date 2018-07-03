/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_ApplicationArtifactIdentifierStringConverter.for_application_with_bounded_context_module_feature_and_recursive_sub_feature
{
    public class when_converting_string_identifier_without_application_resource_in_string : given.an_application_resource_identifier_converter
    {
        static string string_identifier = "Application#BoundedContext.Module";
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => converter.FromString(string_identifier));

        It should_throw_missing_application_resource = () => exception.ShouldBeOfExactType<MissingApplicationArtifact>();
    }
}
