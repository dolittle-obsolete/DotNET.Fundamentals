using System;
using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{
    public class when_initialized_with_type_not_having_a_constructor 
    {
        static Exception result;
        Because of = () => result = Catch.Exception(() => new ApplicationStructureFragment(typeof(SegmentWithoutConstructor)));

        It should_throw_application_location_segment_must_have_a_default_constructor_taking_name = () => result.ShouldBeOfExactType<ApplicationLocationSegmentMustHaveADefaultConstructorTakingName>();
    }
}