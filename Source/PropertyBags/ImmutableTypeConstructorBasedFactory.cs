namespace Dolittle.PropertyBags
{
    using System;
    using Dolittle.Reflection;

    public class ImmutableTypeConstructorBasedFactory : ITypeFactory
    {
        public bool CanBuild(Type type)
        {
            return type.IsImmutable() && type.HasNonDefaultConstructor();
        }

        public bool CanBuild<T>()
        {
            return CanBuild(typeof(T));
        }
    }
}