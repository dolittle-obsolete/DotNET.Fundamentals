namespace Reflection.for_TypeExtensions.for_IsImmutable
{
    public class ClassWithPropertiesWithNoSetter
    {
        public int IntProperty { get; }
        public string StringProperty { get; }
    }

    public class ClassWithPropertiesWithSetters 
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
    }

    public class ClassWithPropertiesWithPrivateSetters 
    {
        public int IntProperty { get; private set; }
        public string StringProperty { get; private set; }
    }

    public class ClassWithMixedProperties 
    {
        public int IntProperty { get; }
        public string StringProperty { get; set; }
    }
}