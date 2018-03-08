using Machine.Specifications;

namespace Applications.for_ApplicationLocation
{
    public class when_equalling_identical_locations : given.identical_locations
    {
        static bool result;
        Because of = () => result = locationA.Equals(locationB);

        It should_be_considered_equal = () => result.ShouldBeTrue();
    }
}