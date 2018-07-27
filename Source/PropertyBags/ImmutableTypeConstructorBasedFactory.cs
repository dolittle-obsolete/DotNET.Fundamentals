namespace Dolittle.PropertyBags
{
    using System;
    using Dolittle.Reflection;

    /// <summary>
    /// Creates a instance of the immutable type, using a constructor to provide all values
    /// </summary>
    public class ImmutableTypeConstructorBasedFactory : ITypeFactory
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
            return type.IsImmutable() && type.HasNonDefaultConstructor();
        }

        /// <inheritdoc />
        public bool CanBuild<T>()
        {
            return CanBuild(typeof(T));
        }
    }
}