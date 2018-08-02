namespace Dolittle.PropertyBags.Specs
{
    using System;
    using Dolittle.PropertyBags;
    using Dolittle.Concepts;

    public class ComplexMutableTypeWithDefaultConstructor : Value<ComplexMutableTypeWithDefaultConstructor>
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }

        public MutableTypeWithDefaultConstructor Nested { get; set; }
    }
}