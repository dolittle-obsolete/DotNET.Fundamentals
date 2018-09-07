using System.Collections.Generic;

namespace Dolittle.PropertyBags.Specs
{
    public class MutableWithEnumerableOfEnumerableOfComplex
    {
        public IEnumerable<IEnumerable<MutableTypeWithDefaultConstructor>> Enumerable{get; set;}
    }
}