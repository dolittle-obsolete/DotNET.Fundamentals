using System;
using Dolittle.Concepts;

namespace Dolittle.Serialization.Protobuf.for_Serializer
{
    public class object_with_concepts
    {
        public static MessageDescription message_description = 
            MessageDescription.DefaultFor<object_with_concepts>();
        
        public ConceptAs<Guid>  a_guid { get; set; }
        public ConceptAs<int> an_integer { get; set; }
        public ConceptAs<float> a_float { get; set; }
        public ConceptAs<double> a_double { get; set; }
        public ConceptAs<string> a_string { get; set; }
        public ConceptAs<DateTime> a_date_time { get; set; }
        public ConceptAs<DateTimeOffset> a_date_time_offset { get; set; }
    }
}