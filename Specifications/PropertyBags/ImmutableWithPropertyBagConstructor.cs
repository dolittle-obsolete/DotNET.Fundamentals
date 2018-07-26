using System;
using Dolittle.PropertyBags;

namespace Dolittle.PropertyBags.Specs
{
    public class ImmutableWithPropertyBagConstructor
    {
        public ImmutableWithPropertyBagConstructor(PropertyBag propertyBag)
        {
            var dynamic = propertyBag as dynamic;
            IntProperty = (int)propertyBag[nameof(IntProperty)];
            StringProperty = (string)propertyBag[nameof(StringProperty)];
            DateTimeProperty = (DateTime)propertyBag[nameof(DateTimeProperty)];
        }

        public int IntProperty { get; }
        public string StringProperty { get; }
        public DateTime DateTimeProperty { get; }
    }
}