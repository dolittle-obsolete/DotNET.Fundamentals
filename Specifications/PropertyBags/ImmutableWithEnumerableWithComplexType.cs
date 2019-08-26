using System.Collections.Generic;

namespace Dolittle.PropertyBags.Specs
{
    public class ImmutableWithEnumerableWithComplexType
    {
        public ImmutableWithEnumerableWithComplexType(IEnumerable<ComplexImmutableWithMultipleParameterConstructor> enumerable)
        {
            Enumerable = enumerable;
        }
        public IEnumerable<ComplexImmutableWithMultipleParameterConstructor> Enumerable{get; }
    }
}