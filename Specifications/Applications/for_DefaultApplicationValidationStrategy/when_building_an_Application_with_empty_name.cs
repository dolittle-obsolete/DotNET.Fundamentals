using System;
using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_DefaultApplicationValidationStrategy
{
    public class when_building_an_Application_with_empty_name : given.an_application_builder_with_empty_name
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => application_builder.Build());

        It should_throw_InvalidApplication = () => result.ShouldBeOfExactType(typeof(InvalidApplication));
        It should_have_an_inner_exception = () => result.InnerException.ShouldNotBeNull();
        It should_have_an_inner_exception_of_MissingApplicationName = () => result.InnerException.ShouldBeOfExactType(typeof(MissingApplicationName));
    }
}