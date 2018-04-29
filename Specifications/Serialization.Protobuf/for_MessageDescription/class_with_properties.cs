using System.Reflection;

namespace Dolittle.Serialization.Protobuf.for_MessageDescription
{
    public class class_with_properties
    {
        public static PropertyInfo first_property = typeof(class_with_propery).GetProperty("FirstProperty");
        public static PropertyInfo second_property = typeof(class_with_propery).GetProperty("SecondProperty");
        public int FirstProperty {  get; set; }
        public string SecondProperty {  get; set; }
    }
}