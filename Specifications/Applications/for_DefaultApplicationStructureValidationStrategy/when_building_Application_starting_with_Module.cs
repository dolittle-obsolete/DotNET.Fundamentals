using System;
using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_DefaultApplicationStructureValidationStrategy
{
    public class when_building_Application_starting_with_Module : given.a_standard_ApplicationBuilder
    {
        static Exception result;

        Establish context = () => application_builder = application_builder
                                    .WithStructureStartingWith<Module>(m => m.Required);


        Because of = () => result = Catch.Exception(() => application_builder.Build(new given.SimplifiedApplicationValidationStrategy()));

        It should_have_an_exception = () => result.ShouldNotBeNull();
        It should_throw_InvalidApplicationStructure = () => result.ShouldBeOfExactType(typeof(InvalidApplicationStructure));
        It should_have_an_inner_exception = () => result.InnerException.ShouldNotBeNull();
        It should_have_an_inner_exception_of_ApplicationStructureMustStartWithABoundedContext = () => result.InnerException.ShouldBeOfExactType(typeof(ApplicationStructureMustStartWithABoundedContext));
    }
}