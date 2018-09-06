using System.Collections.Generic;

namespace Dolittle.PropertyBags.Specs
{
    public class MutableWithEnumerableOfEnumerableOfPrimitive
    {
        public IEnumerable<IEnumerable<string>> Enumerable {get; set;}
    }
}