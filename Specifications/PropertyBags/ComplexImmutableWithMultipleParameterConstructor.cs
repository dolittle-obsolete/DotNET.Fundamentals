namespace Dolittle.PropertyBags.Specs
{
    using System;
    using System.Dynamic;
    using Dolittle.Concepts;

    public class ComplexImmutableWithMultipleParameterConstructor : Value<ComplexImmutableWithMultipleParameterConstructor>
    {
        public ComplexImmutableWithMultipleParameterConstructor(int intProperty, string stringProperty, DateTime dateTimeProperty, ImmutableWithMultipleParameterConstructor nested)
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