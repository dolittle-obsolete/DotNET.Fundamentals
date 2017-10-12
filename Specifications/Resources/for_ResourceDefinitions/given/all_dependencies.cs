using System.Collections.Generic;
using doLittle.Types;
using Machine.Specifications;
using Moq;

namespace doLittle.Resources.for_ResourceDefinitions.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<ICanDefineResource>> resource_definers_instances;
        protected static List<ICanDefineResource> resource_definers;
        protected static Mock<IResourceDefinitionBuilderFactory> resource_definition_builder_factory;

        Establish context = () =>
        {
            resource_definers = new List<ICanDefineResource>();
            resource_definers_instances = new Mock<IInstancesOf<ICanDefineResource>>();
            resource_definers_instances.Setup(r => r.GetEnumerator()).Returns(() => resource_definers.GetEnumerator());
            resource_definition_builder_factory = new Mock<IResourceDefinitionBuilderFactory>();
        };
    }
}