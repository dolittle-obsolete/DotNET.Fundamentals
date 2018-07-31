namespace Dolittle.PropertyBags.Specs
{
    using System;
    using Dolittle.PropertyBags;
    using Dolittle.Concepts;

    public class MutableTypeWithNoDefaultConstructor : Value<MutableTypeWithNoDefaultConstructor>
    {
        public MutableTypeWithNoDefaultConstructor(int intProperty, string stringProperty, DateTime dateTimeProperty)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
            DateTimeProperty = dateTimeProperty;
        }

        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }

        public string NotSetFromAConstuctor { get; set; }
        public int AReadOnlyProperty => 10;
    }
}