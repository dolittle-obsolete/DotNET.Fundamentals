using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_DefaultApplicationValidationStrategy.given
{
    public class an_application_builder_with_empty_name
    {
        protected static IApplicationBuilder application_builder;

        Establish context = () =>
        {
            application_builder = new ApplicationBuilder(""); 
        };
    }
}