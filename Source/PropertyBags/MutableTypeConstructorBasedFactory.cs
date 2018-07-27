namespace Dolittle.PropertyBags
{
    using System;
    using Dolittle.Reflection;

    /// <summary>
    /// A factory that instantiates a type using a constructor then populates other mutable properties
    /// </summary>
    public class MutableTypeConstructorBasedFactory : ITypeFactory
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
            return !type.IsImmutable() && !type.HasDefaultConstructor();
        }

        /// <inheritdoc />  
        public bool CanBuild<T>()
        {
            return CanBuild(typeof(T));
        }
    }    
}