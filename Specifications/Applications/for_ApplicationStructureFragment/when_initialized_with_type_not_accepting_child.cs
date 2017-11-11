using System;
using Machine.Specifications;

namespace doLittle.Applications.for_ApplicationStructureFragment
{
    public class when_initialized_with_type_not_accepting_child
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() =>
                                        new ApplicationStructureFragment(
                                            typeof(Fragment),
                                            new IApplicationStructureFragment[] {
                                                new ApplicationStructureFragment(typeof(FragmentWithBelonging))
                                            }
                                        )
                                    );
        It should_throw_parent_application_structure_fragment_must_be_able_to_hold_type = () => result.ShouldBeOfExactType<ParentApplicationStructureFragmentMustBeAbleToHoldType>();
    }
}