using Machine.Specifications;

namespace doLittle.Applications.for_ApplicationStructureFragment
{
    public class when_initialized_with_parent
    {
        static ApplicationStructureFragment fragment;
        static ApplicationStructureFragment parent;

        Establish context = () => parent = new ApplicationStructureFragment(
                                                typeof(FragmentThatCanHoldFragmentThatBelongsToIt)
                                           );

        Because of = () => fragment = new ApplicationStructureFragment(
                                typeof(FragmentBelongingToFragmentThatCanHoldIt), 
                                parent
                            );

        It should_hold_the_type = () => fragment.Type.ShouldEqual(typeof(FragmentBelongingToFragmentThatCanHoldIt));
        It should_hold_the_parent = () => fragment.Parent.ShouldEqual(parent);
    }
}