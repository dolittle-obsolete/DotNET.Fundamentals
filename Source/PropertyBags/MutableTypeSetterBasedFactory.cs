namespace Dolittle.PropertyBags
{
    using System;
    using Dolittle.Reflection;

    /// <summary>
    /// Creates a mutable object using the default constructor then setting all the mutable properties
    /// </summary>
    public class MutableTypeSetterBasedFactory : ITypeFactory
    {
        /// <inheritdoc />  
        public object Build(Type type, IObjectFactory objectFactory, PropertyBag source)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />  
        public T Build<T>(IObjectFactory objectFactory, PropertyBag source)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />  
        public bool CanBuild(Type type)
        {
            return !type.IsImmutable() && type.HasDefaultConstructor();
        }

        /// <inheritdoc />  
        public bool CanBuild<T>()
        {
            return CanBuild(typeof(T));
        }
    }
}