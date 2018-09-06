using System.Collections.Generic;
namespace Dolittle.PropertyBags.Specs
{
    public class MutableWithEnumerableWithComplexElements
    {

        public IEnumerable<ComplexImmutableWithMultipleParameterConstructor> Enumerable {get; set;}
    }
}