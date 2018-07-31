namespace Dolittle.PropertyBags
{
    using System;
    using System.Linq;
    using Dolittle.Reflection;
    using System.Collections.Concurrent;

    /// <summary>
    /// Creates a mutable object using the default constructor then setting all the mutable properties
    /// </summary>
    public class MutableTypeSetterBasedFactory : ITypeFactory
    {
        ConcurrentDictionary<Type,InstancePropertySetter> _setters = new ConcurrentDictionary<Type, InstancePropertySetter>();
        /// <inheritdoc />  
        public object Build(Type type, IObjectFactory objectFactory, PropertyBag source)
        {
            var setter =_setters.GetOrAdd(type, (t) => new InstancePropertySetter(type, objectFactory));
            var instance = Activator.CreateInstance(type);
            setter.Populate(instance, source);
            return instance;
        }

        /// <inheritdoc />  
        public T Build<T>(IObjectFactory objectFactory, PropertyBag source)
        {
            return (T)Build(typeof(T),objectFactory,source);
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