using System;
using Machine.Specifications;

namespace doLittle.Applications.for_ApplicationStructureFragment
{
    public class when_initialized_with_child_not_belonging_to_type
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() =>
                                        new ApplicationStructureFragment(
                                            typeof(Segment),
                                            new IApplicationStructureFragment[] {
                                                new ApplicationStructureFragment(typeof(SegmentWithoutBelonging))
                                            }
                                        )
                                    );

        It should_throw_application_structure_fragment_must_belong_to_parent = () => result.ShouldBeOfExactType<ApplicationStructureFragmentMustBelongToParent>();
    }
}