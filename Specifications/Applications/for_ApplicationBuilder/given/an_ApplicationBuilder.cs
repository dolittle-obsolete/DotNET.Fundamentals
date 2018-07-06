using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_ApplicationBuilder.given
{
    public class an_ApplicationBuilder
    {
        protected const string application_name = "Application";
        protected static IApplicationBuilder application_builder;

        Establish of = () => 
        {
            application_builder = Application.WithName(application_name);
        };
    }
}