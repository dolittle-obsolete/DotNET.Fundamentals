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

    public class Complex : Value<Complex>
    {  
        public Complex(Guid concept, Immutable immutable, int primitive, Dictionary<string, object> content)
        {
            Concept = concept;
            Immutable = immutable;
            Primitive = primitive;
            Content = content;
        }

        public Guid Concept { get; }

        public Immutable Immutable { get; }

        public int Primitive { get; }

        public IDictionary<string, object> Content { get; }

        public override bool Equals(Complex other)
        {
            if(other == null)
                return false;

            if (this.Concept != other.Concept || this.Immutable != other.Immutable || this.Primitive != other.Primitive)
                return false;

            if ((this.Content != null && other.Content == null) || (this.Content == null && other.Content != null))
                return false;

            if(this.Content == null && other.Content == null)
                return true;

            return this.Content.Count == other.Content.Count
                        && this.Content.Keys.All(key => other.Content.ContainsKey(key) 
                                                                && this.Content[key].Equals(Convert.ChangeType(other.Content[key], this.Content[key].GetType()))); //using coercion here to deal with unboxing issues
        }
    }
}