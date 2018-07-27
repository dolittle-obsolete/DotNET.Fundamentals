namespace Dolittle.PropertyBags.Specs
{
    using System;
    using Dolittle.PropertyBags;
    using Dolittle.Concepts;
    public class MutableTypeWithDefaultConstructor : Value<MutableTypeWithDefaultConstructor>
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
    }
}