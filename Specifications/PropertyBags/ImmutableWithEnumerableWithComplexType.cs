using System.Collections.Generic;

namespace Dolittle.PropertyBags.Specs
{
    public class ImmutableWithEnumerableWithComplexType
    {
        public ImmutableWithEnumerableWithComplexType(IEnumerable<MutableTypeWithDefaultConstructor> enumerable)
        {
            Enumerable = enumerable;
        }
        public IEnumerable<MutableTypeWithDefaultConstructor> Enumerable{get; }
    }
}