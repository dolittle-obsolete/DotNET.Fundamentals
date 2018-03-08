using Machine.Specifications;

namespace Applications.for_ApplicationLocation
{

    public class when_comparing_different_locations : given.different_locations
    {
        static int result;
        Because of = () => result = locationA.CompareTo(locationB);

        It should_be_not_considered_equal = () => result.ShouldNotEqual(0);
    }        
}