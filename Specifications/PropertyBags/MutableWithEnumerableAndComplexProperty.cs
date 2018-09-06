using System.Collections.Generic;

namespace Dolittle.PropertyBags.Specs
{
    public class MutableWithEnumerableAndComplexProperty
    {
        
        public IEnumerable<string> Enumerable {get; set; }
        public ImmutableWithConceptProperty Concept {get; set;}
    }
}