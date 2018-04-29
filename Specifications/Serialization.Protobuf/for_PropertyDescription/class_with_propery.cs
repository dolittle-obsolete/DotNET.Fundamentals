using System.Reflection;

namespace Dolittle.Serialization.Protobuf.for_PropertyDescription
{
    public class class_with_propery
    {
        public static PropertyInfo some_property = typeof(class_with_propery).GetProperty("SomeProperty");
        public int SomeProperty {  get; set; }
    }
}