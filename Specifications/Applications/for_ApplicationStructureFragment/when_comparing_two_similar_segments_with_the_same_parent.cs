
using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_ApplicationStructureFragment
{
    public class when_comparing_two_similar_segments_with_the_same_parent
    {
        static IApplicationStructureFragment parent;
        static IApplicationStructureFragment child;
        static IApplicationStructureFragment fragment_1, fragment_2;

        Establish context = () => 
        {
            child = new ApplicationStructureFragment(typeof(Module));
            parent = new ApplicationStructureFragment(typeof(BoundedContext), new IApplicationStructureFragment[]
            {child});

            fragment_1 = new ApplicationStructureFragment(typeof(Module), parent);
            fragment_2 = new ApplicationStructureFragment(typeof(Module), parent);
            
        };

        It should_be_the_same = () => fragment_1.Equals(fragment_2).ShouldBeTrue();
        It should_have_the_same_hash_code = () => fragment_1.GetHashCode().ShouldEqual(fragment_2.GetHashCode());
        It should_have_the_same_parent = () => fragment_1.Parent.ShouldEqual(fragment_2.Parent);
        It should_have_the_correct_parrent = () => fragment_1.Parent.ShouldEqual(parent);
    }
}