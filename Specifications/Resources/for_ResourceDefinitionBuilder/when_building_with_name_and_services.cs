using System;
using System.Linq;
using Machine.Specifications;

namespace doLittle.Resources.for_ResourceDefinitionBuilder
{
    public class when_building_with_name_and_services : given.a_resource_definition_builder
    {
        const string name = "Something";

        static IResourceDefinition result;

        Establish context = () => builder.WithName(name).Requires<string>().Requires<Exception>();

        Because of = () => result = builder.Build();

        It should_forward_name = () => result.Name.ShouldEqual(name);
        It should_include_first_service = () => result.Services.ToArray()[0].Service.ShouldEqual(typeof(string));
        It should_include_second_service = () => result.Services.ToArray()[1].Service.ShouldEqual(typeof(Exception));
        
    }
}