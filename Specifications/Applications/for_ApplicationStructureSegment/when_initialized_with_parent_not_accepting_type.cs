using System;
using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{
    public class when_initialized_with_parent_not_accepting_type
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() =>
                                        new ApplicationStructureFragment(
                                            typeof(SegmentWithBelonging),
                                            new ApplicationStructureFragment(typeof(Segment))
                                        )
                                    );
        It should_throw_parent_application_structure_fragment_must_be_able_to_hold_type = () => result.ShouldBeOfExactType<ParentApplicationStructureFragmentMustBeAbleToHoldType>();
    }
}