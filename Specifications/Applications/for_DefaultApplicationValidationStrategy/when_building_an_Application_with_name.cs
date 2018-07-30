using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_DefaultApplicationValidationStrategy
{
    public class when_building_an_Application_with_name : given.an_application_builder_with_name
    {
        static IApplication application;

        Because of = () =>
        {
            application = application_builder.Build();
        };

        It should_hold_the_application_name = () => application.Name.Value.ShouldEqual(application_name);
    }
}