using Machine.Specifications;

namespace Applications.for_ApplicationLocation
{

    public class when_equalling_different_locations_using_equals_operator : given.different_locations
    {
        static bool result;
        Because of = () => result = locationA == locationB;

        It should_not_be_considered_equal = () => result.ShouldBeFalse();
    }    
}