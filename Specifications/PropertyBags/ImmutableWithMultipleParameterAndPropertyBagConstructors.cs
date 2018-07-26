using System;
using Dolittle.PropertyBags;

namespace Dolittle.PropertyBags.Specs
{

    public class ImmutableWithMultipleParameterAndPropertyBagConstructors
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