using Machine.Specifications;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{
    public class when_initialized_with_parent
    {
        static ApplicationStructureFragment fragment;
        static ApplicationStructureFragment parent;

        Establish context = () => parent = new ApplicationStructureFragment(
                                                typeof(SegmentThatCanHoldSegmentThatBelongsToIt)
                                           );

        Because of = () => fragment = new ApplicationStructureFragment(
                                typeof(SegmentBelongingToSegmentThatCanHoldIt), 
                                parent
                            );

        It should_hold_the_type = () => fragment.Type.ShouldEqual(typeof(SegmentBelongingToSegmentThatCanHoldIt));
        It should_hold_the_parent = () => fragment.Parent.ShouldEqual(parent);
    }
}