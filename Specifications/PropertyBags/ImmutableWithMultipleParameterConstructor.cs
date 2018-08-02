namespace Dolittle.PropertyBags.Specs
{
    using System;
    using Dolittle.PropertyBags;
    using Dolittle.Concepts;
    public class ImmutableWithMultipleParameterConstructor : Value<ImmutableWithMultipleParameterConstructor>
    {
        public ImmutableWithMultipleParameterConstructor(int intProperty, string stringProperty, DateTime dateTimeProperty, DateTime? nullableDateTime)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
            DateTimeProperty = dateTimeProperty;
            NullableDateTime = nullableDateTime;
        } 

        public int IntProperty { get; }
        public string StringProperty { get; }
        public DateTime DateTimeProperty { get; }
        public DateTime? NullableDateTime { get; }
    }
}