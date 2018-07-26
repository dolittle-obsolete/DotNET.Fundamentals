using System;
using Dolittle.PropertyBags;

namespace Dolittle.PropertyBags.Specs
{

    public class MutableTypeWithDefaultConstructor
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
    }
}