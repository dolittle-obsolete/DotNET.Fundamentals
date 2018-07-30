namespace Dolittle.PropertyBags
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class TypeExtensions
    {
        public static bool HasPropertyBagConstructor(this Type type)
        {
            var ctor = type.GetTypeInfo().DeclaredConstructors.SingleOrDefault(c => c.GetParameters().Length == 1 
                                                                                && c.GetParameters().First().ParameterType == typeof(PropertyBag));
            return ctor != null;
        }

        public static ConstructorInfo GetPropertyBagConstructor(this Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors.SingleOrDefault(c => c.GetParameters().Length == 1 
                                                                                && c.GetParameters().First().ParameterType == typeof(PropertyBag));
        }
    }
}