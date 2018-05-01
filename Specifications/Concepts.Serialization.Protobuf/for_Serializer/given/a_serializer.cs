using System;
using System.Collections.Generic;
using Machine.Specifications;
using Dolittle.Serialization.Protobuf;
using Dolittle.Types;
using Moq;

namespace Dolittle.Concepts.Serialization.Protobuf.for_Serializer.given
{
    public class a_serializer : all_dependencies
    {
        protected static Serializer serializer;
        protected static ConceptConverter concept_value_converter;

        Establish context = () => 
        {
            concept_value_converter = new ConceptConverter();
            var converters = new List<IValueConverter>(new[] {
                concept_value_converter
            });

            var value_converters = new Mock<IValueConverters>();
            value_converters.Setup(_ => _.CanConvert(Moq.It.IsAny<Type>())).Returns(true);
            value_converters.Setup(_ => _.GetConverterFor(Moq.It.IsAny<Type>())).Returns(concept_value_converter);
            serializer = new Serializer(message_descriptions.Object, value_converters.Object);
        };
    }
}