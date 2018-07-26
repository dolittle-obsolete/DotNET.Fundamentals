using System;
using Dolittle.PropertyBags;

namespace Dolittle.PropertyBags.Specs
{
    public class MutableTypeWithNoDefaultConstructor
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
    }
}