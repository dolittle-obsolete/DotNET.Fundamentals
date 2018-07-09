using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_ApplicationStructureFragment
{
    public class when_comparing_two_segments_with_different_children
    {
        
        static IApplicationStructureFragment fragment_1, fragment_2;

        Establish context = () => 
        {
            fragment_1 = new ApplicationStructureFragment(typeof(BoundedContext), 
                new IApplicationStructureFragment[] 
                {new ApplicationStructureFragment(typeof(Module), true, true)});
            fragment_2 = new ApplicationStructureFragment(typeof(BoundedContext), 
                new IApplicationStructureFragment[] 
                {new ApplicationStructureFragment(typeof(Module), false, false)});
        };

        It should_not_be_the_same = () => fragment_1.Equals(fragment_2).ShouldBeFalse();
        It should_not_have_the_same_hash_code = () => fragment_1.GetHashCode().ShouldNotEqual(fragment_2.GetHashCode());
    }
}