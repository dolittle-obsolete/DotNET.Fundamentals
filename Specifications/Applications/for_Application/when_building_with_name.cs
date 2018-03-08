using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Applications.Specs.for_Application
{
    public class when_starting_building_with_name
    {
        const string application_name = "My Application";
        static IApplication result;

        Because of = () => result = Application.WithName(application_name).Build();

        It should_hold_name = () => ((string) result.Name).ShouldEqual(application_name);
    }
}
