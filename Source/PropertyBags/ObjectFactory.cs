namespace Dolittle.PropertyBags
{
    using System.Collections.Generic;
    using System.Linq;
    using Dolittle.Types;
    using Dolittle.Reflection;
    using System;
    using System.Collections.Concurrent;

    public class ObjectFactory : IObjectFactory
    {
        IInstancesOf<ITypeFactory> _factories;
        List<ITypeFactory> _userDefinedFactories = new List<ITypeFactory>();
        List<ITypeFactory> _systemFactories = new List<ITypeFactory>();
        ConcurrentDictionary<Type,Lazy<ITypeFactory>> _cachedFactories = new ConcurrentDictionary<Type,Lazy<ITypeFactory>>();

        public ObjectFactory(IInstancesOf<ITypeFactory> factories)
        {
            _factories = factories;
            foreach(var f in factories)
            {
                if(IsUserDefined(f))
                {
                    _userDefinedFactories.Add(f);
                } 
                else 
                {
                    _systemFactories.Add(f);
                }
            }
        }
        public object Build(Type type, PropertyBag source)
        {
            var lazyFactory = _cachedFactories.GetOrAdd(type, (t) => new Lazy<ITypeFactory>(() => GetTypeFactoryForType(t)));
            return lazyFactory.Value.Build(type, this, source);
        }

        public T Build<T>(PropertyBag source)
        {
            return (T)Build(typeof(T),source);
        }

        bool IsUserDefined(ITypeFactory instance)
        {
            return instance.GetType().ImplementsOpenGeneric(typeof(IUserDefinedTypeFactory<>));
        }

        ITypeFactory GetTypeFactoryForType(Type type)
        {
            ITypeFactory typeFactory;
            try
            {
                typeFactory = _userDefinedFactories.SingleOrDefault(f => f.CanBuild(type));
                if(typeFactory != null)
                    return typeFactory;
            }
            catch(InvalidOperationException ex)
            {
                throw new MultipleFactoriesForType($"{type.FullName} has multiple user defined factories to build it.  A type can only have one factory defined.",ex);
            }

            try
            {
                typeFactory = _systemFactories.SingleOrDefault(f => f.CanBuild(type));
                if(typeFactory != null)
                    return typeFactory;
                
                throw new NoFactoriesForType($"{type.FullName} has no factories to build it.");
            }
            catch(InvalidOperationException ex)
            {
                throw new MultipleFactoriesForType($"{type.FullName} has multiple built in factories to build it.  Check your type definition.",ex);
            }
        }
    }
}