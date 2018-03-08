using Machine.Specifications;

namespace Applications.for_ApplicationLocation
{

    public class when_equalling_identical_locations_using_equals_operator : given.identical_locations
    {
        static bool result;
        Because of = () => result = locationA == locationB;

        It should_be_considered_equal = () => result.ShouldBeTrue();
    }    
}