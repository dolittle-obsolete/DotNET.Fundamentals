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

    public class Immutable : Value<Immutable>
    {
        public String Label { get; }
        public Guid Guid { get; }

        public Immutable(Guid guid, String label)
        {
            Label = label;
            Guid = guid;
        }
    }
}