namespace Dolittle.PropertyBags
{
    using System;
    using Dolittle.Reflection;

    public class MutableTypeConstructorBasedFactory : ITypeFactory
    {
        public bool CanBuild(Type type)
        {
            return !type.IsImmutable() && !type.HasDefaultConstructor();
        }

        public bool CanBuild<T>()
        {
            return CanBuild(typeof(T));
        }
    }    
}