using System.Collections.Generic;
using Dolittle.DependencyInversion;
using Dolittle.Serialization.Json;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Serialization.Json.Specs.for_Serializer.given
{
    public class a_serializer
    {
        protected static Serializer serializer;
        protected static Mock<IInstancesOf<ICanProvideConverters>> converter_provider_instances;
        protected static List<ICanProvideConverters> converter_providers;

        Establish context = () =>
                                {
                                    converter_providers = new List<ICanProvideConverters>();
                                    
                                    converter_provider_instances = new Mock<IInstancesOf<ICanProvideConverters>>();
                                    converter_provider_instances.Setup(c => c.GetEnumerator()).Returns(() => converter_providers.GetEnumerator());
                                    serializer = new Serializer(converter_provider_instances.Object);
                                };
    }
}
