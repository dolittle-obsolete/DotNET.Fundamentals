using System.Collections.Generic;
using Dolittle.DependencyInversion;
using Dolittle.Serialization.Json;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Resources.Configuration.Specs.for_TenantResourceManager.given
{
    public class a_serializer
    {
        protected static Serializer serializer;
        protected static Mock<IContainer> container_mock;
        protected static Mock<IInstancesOf<ICanProvideConverters>> converter_provider_instances;
        protected static List<ICanProvideConverters> converter_providers;

        Establish context = () =>
                                {
                                    container_mock = new Mock<IContainer>();
                                    converter_providers = new List<ICanProvideConverters>();
                                    
                                    converter_provider_instances = new Mock<IInstancesOf<ICanProvideConverters>>();
                                    converter_provider_instances.Setup(c => c.GetEnumerator()).Returns(() => converter_providers.GetEnumerator());
                                    serializer = new Serializer(container_mock.Object, converter_provider_instances.Object);
                                };

    }
}