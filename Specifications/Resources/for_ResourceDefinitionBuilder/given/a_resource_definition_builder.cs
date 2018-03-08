using Machine.Specifications;

namespace Dolittle.Resources.for_ResourceDefinitionBuilder.given
{
    public class a_resource_definition_builder
    {
        protected static ResourceDefinitionBuilder builder;

        Establish context = () => builder = new ResourceDefinitionBuilder();
        
    }
}