using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_Application
{
    public class when_comparing_applications_with_different_names
    {
        const string name_a = "ApplicationA";
        const string name_b = "ApplicationB";
        static IApplication application_a, application_b;

        Establish context = () => 
        {
            application_a = Application.WithName(name_a)
                .Build();
            application_b = Application.WithName(name_b)
                .Build();
        };

        It should_hold_name_a = () => application_a.Name.Value.ShouldEqual(name_a);
        It should_hold_name_b = () => application_b.Name.Value.ShouldEqual(name_b);
        It should_not_be_the_same = () => application_a.Equals(application_b).ShouldBeFalse();
        It should_not_have_the_same_name = () => application_a.Name.Equals(application_b.Name).ShouldBeFalse();
        It should_have_different_hash_codes => () => application_a.GetHashCode().Equals(application_b.GetHashCode()).ShouldBeFalse();
    }
}