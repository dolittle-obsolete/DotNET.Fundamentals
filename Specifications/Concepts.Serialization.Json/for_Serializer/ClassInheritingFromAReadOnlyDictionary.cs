using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Dolittle.Concepts.Serialization.Json.Specs.for_Serializer
{
    public class ClassInheritingFromAReadOnlyDictionary : ReadOnlyDictionary<string, string>
    {
        public ClassInheritingFromAReadOnlyDictionary(IDictionary<string, string> dictionary) : base(dictionary)
        {
        }
    }
}