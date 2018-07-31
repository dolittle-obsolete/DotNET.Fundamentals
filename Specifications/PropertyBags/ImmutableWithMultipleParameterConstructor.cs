namespace Dolittle.PropertyBags.Specs
{
    using System;
    using Dolittle.PropertyBags;
    using Dolittle.Concepts;
    public class ImmutableWithMultipleParameterConstructor : Value<ImmutableWithMultipleParameterConstructor>
    {
        public ImmutableWithMultipleParameterConstructor(int intProperty, string stringProperty, DateTime dateTimeProperty)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
            DateTimeProperty = dateTimeProperty;
        } 

        public int IntProperty { get; }
        public string StringProperty { get; }
        public DateTime DateTimeProperty { get; }
    }

    public class ImmutableWithConceptProperty : Value<ImmutableWithConceptProperty>
    {
        public ImmutableWithConceptProperty(IntConcept intProperty, StringConcept stringProperty)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
        } 

        public int IntProperty { get; }
        public string StringProperty { get; }
    }

    public class StringConcept : ConceptAs<string>
    {
        public StringConcept(string value) => Value = value;        

        public static implicit operator StringConcept(string value)
        {
            return new StringConcept(value);
        }
    }

    public class IntConcept : ConceptAs<int>
    {
        public static implicit operator IntConcept(int value)
        {
            return new IntConcept { Value = value };
        }
    }
}