using System.Collections.Generic;

namespace Dolittle.PropertyBags.Specs
{
    public class MutableWithEnumerableAndPrimitiveType
    {
        public IEnumerable<string> Enumerable {get; set;}

        public string String {get; set;}
    }
}