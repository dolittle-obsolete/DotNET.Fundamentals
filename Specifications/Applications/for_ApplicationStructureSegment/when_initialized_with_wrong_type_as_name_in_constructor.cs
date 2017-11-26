using System;
using Machine.Specifications;

namespace doLittle.Applications.for_ApplicationStructureFragment
{
    public class when_initialized_with_wrong_type_as_name_in_constructor 
    {
        static Exception result;
        Because of = () => result = Catch.Exception(() => new ApplicationStructureFragment(typeof(SegmentWithoutNameConstructor)));

        It should_throw_application_location_segment_must_have_a_default_constructor_taking_name = () => result.ShouldBeOfExactType<ApplicationLocationSegmentMustHaveADefaultConstructorTakingName>();
    }
}