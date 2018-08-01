namespace Dolittle.PropertyBags.Specs
{
    using System;
    using Dolittle.PropertyBags;
    using Dolittle.Concepts;

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
}