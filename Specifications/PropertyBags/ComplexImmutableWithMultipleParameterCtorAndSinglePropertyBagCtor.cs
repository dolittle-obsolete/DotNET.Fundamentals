namespace Dolittle.PropertyBags.Specs
{
    using System;
    using Dolittle.Concepts;

    public class ComplexImmutableWithMultipleParameterCtorAndSinglePropertyBagCtor
        : Value<ComplexImmutableWithMultipleParameterConstructor>
    {
        public ComplexImmutableWithMultipleParameterCtorAndSinglePropertyBagCtor(PropertyBag source)
        {
            var propertyBag = (dynamic)source;
            IntProperty = propertyBag.IntProperty;
            StringProperty = propertyBag.StringProperty;
            DateTimeProperty = propertyBag.DateTimeProperty;
            Nested = propertyBag.Nested;
        }

        public ComplexImmutableWithMultipleParameterCtorAndSinglePropertyBagCtor(int intProperty, string stringProperty, DateTime dateTimeProperty, ImmutableWithMultipleParameterConstructor nested)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
            DateTimeProperty = dateTimeProperty;
            Nested = nested;
        } 

        public int IntProperty { get; }
        public string StringProperty { get; }
        public DateTime DateTimeProperty { get; }       
        public ImmutableWithMultipleParameterConstructor Nested { get; } 
    }
}