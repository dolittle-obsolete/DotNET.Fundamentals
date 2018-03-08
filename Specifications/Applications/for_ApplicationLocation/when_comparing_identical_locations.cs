using Machine.Specifications;

namespace Applications.for_ApplicationLocation
{

    public class when_comparing_identical_locations : given.identical_locations
    {
        static int result;
        Because of = () => result = locationA.CompareTo(locationB);

        It should_be_considered_equal = () => result.ShouldEqual(0);
    }        
}