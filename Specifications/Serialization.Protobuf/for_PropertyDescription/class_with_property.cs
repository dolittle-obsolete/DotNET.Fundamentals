using System.Reflection;

namespace Dolittle.Serialization.Protobuf.for_PropertyDescription
{
    public class class_with_property
    {
        public static PropertyInfo some_property = typeof(class_with_property).GetProperty("SomeProperty");
        public int SomeProperty {  get; set; }
    }
}