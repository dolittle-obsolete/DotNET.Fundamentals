using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_ApplicationStructureFragment
{
    public class when_comparing_two_similar_segments_with_no_children
    {
        static IApplicationStructureFragment fragment_1, fragment_2;

        Establish context = () => 
        {
            fragment_1 = new ApplicationStructureFragment(typeof(BoundedContext));
            fragment_2 = new ApplicationStructureFragment(typeof(BoundedContext));
        };

        It should_be_the_same = () => fragment_1.Equals(fragment_2).ShouldBeTrue();
        It should_have_the_same_hash_code = () => fragment_1.GetHashCode().ShouldEqual(fragment_2.GetHashCode());
    }
}