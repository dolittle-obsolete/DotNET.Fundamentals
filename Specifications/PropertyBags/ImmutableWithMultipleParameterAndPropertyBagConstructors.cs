namespace Dolittle.PropertyBags.Specs
{
    using System;
    using Dolittle.PropertyBags;
    using Dolittle.Concepts;
    public class ImmutableWithMultipleParameterAndPropertyBagConstructors : Value<ImmutableWithMultipleParameterAndPropertyBagConstructors>
    {
        public ImmutableWithMultipleParameterAndPropertyBagConstructors(int intProperty, string stringProperty, DateTime dateTimeProperty)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
            DateTimeProperty = dateTimeProperty;
        } 

        public ImmutableWithMultipleParameterAndPropertyBagConstructors(PropertyBag propertyBag) : this(
            (int)((dynamic)propertyBag).IntProperty,
            (string)((dynamic)propertyBag).StringProperty,
            (DateTime)((dynamic)propertyBag).DateTimeProperty
        )
        {}

        public int IntProperty { get; }
        public string StringProperty { get; }
        public DateTime DateTimeProperty { get; }
    }
}