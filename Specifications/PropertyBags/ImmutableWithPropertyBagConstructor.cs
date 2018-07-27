namespace Dolittle.PropertyBags.Specs
{
    using System;
    using Dolittle.PropertyBags;
    using Dolittle.Concepts;
    public class ImmutableWithPropertyBagConstructor : Value<ImmutableWithPropertyBagConstructor>
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