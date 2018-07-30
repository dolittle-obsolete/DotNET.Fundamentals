using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_DefaultApplicationValidationStrategy.given
{
    public class an_application_builder_with_name
    {
        protected readonly static string application_name = "ApplicationName";
        protected static IApplicationBuilder application_builder;

        Establish context = () =>
        {
            application_builder = new ApplicationBuilder(application_name);
        };
    }
}