using System;
using Dolittle.PropertyBags;

namespace Dolittle.PropertyBags.Specs
{

    public class ImmutableWithMultipleParameterConstructor
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
}