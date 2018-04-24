using System;
using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{

    public class when_initialized_with_invalid_type
    {
        static Exception result;
        Because of = () => result = Catch.Exception(() => new ApplicationStructureFragment(typeof(string)));

        It should_throw_application_structure_fragment_must_be_application_location = () => result.ShouldBeOfExactType<ApplicationStructureFragmentMustBeApplicationLocation>();
    }
}