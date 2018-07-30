using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_DefaultApplicationStructureValidationStrategy.given
{
    public class a_standard_ApplicationBuilder
    {
        protected readonly static ApplicationName application_name = "Application"; 
        protected static IApplicationBuilder application_builder;
        Establish context = () => 
        {   
            application_builder = Application.WithName(application_name);
        };
    }
}