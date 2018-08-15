namespace Dolittle.Serialization.Json.Specs.for_Serializer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Machine.Specifications; 
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Dolittle.PropertyBags;
    using Dolittle.Concepts;
    using Serialization.Json;

    public class GuidConcept : ConceptAs<Guid>
    {
        public static implicit operator GuidConcept(Guid value)
        {
            return new GuidConcept{ Value = value };
        }
    }
}