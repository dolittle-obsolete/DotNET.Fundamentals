using System.Collections.Generic;

namespace Dolittle.PropertyBags.Specs
{
    public class ImmutableWithEnumerableWithPrimitiveType
    {
        public ImmutableWithEnumerableWithPrimitiveType(IEnumerable<string> enumerable)
        {
            Enumerable = enumerable;
        }
        public IEnumerable<string> Enumerable{get; }
    }
}