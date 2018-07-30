namespace Dolittle.PropertyBags
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using Dolittle.Execution;
    using Dolittle.Reflection;
    using Dolittle.Collections;
    using Dolittle.Strings;

    /// <summary>
    /// Creates a instance of the immutable type, using a constructor to provide all values
    /// </summary>
    [Singleton]
    public class ImmutableTypeConstructorBasedFactory : ITypeFactory
    {
        ConcurrentDictionary<Type,PropertyBagToTypeInstanceFactory> _factories = new ConcurrentDictionary<Type, PropertyBagToTypeInstanceFactory>();
        private readonly IConstructorProvider _provider;

        public ImmutableTypeConstructorBasedFactory(IConstructorProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc />
        public object Build(Type type, IObjectFactory objectFactory, PropertyBag source)
        {
            var fac =_factories.GetOrAdd(type, (t) => new PropertyBagToTypeInstanceFactory(_provider.GetFor(type), objectFactory));
            return fac.Build(source);
        }

        /// <inheritdoc />
        public T Build<T>(IObjectFactory objectFactory, PropertyBag source)
        {
            return (T)Build(typeof(T),objectFactory,source);
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

        private class PropertyBagToTypeInstanceFactory
        {
            Instantiator.InstanceActivator _activator;
            List<Func<PropertyBag,object>> _populators = new List<Func<PropertyBag, object>>();
            private readonly IObjectFactory _factory;

            //TODO: strategy for selecting constructor
            //TODO: strategy for determining ctr param => property name 
            public PropertyBagToTypeInstanceFactory(ConstructorInfo ctor, IObjectFactory factory )
            {
                ConstructorInfo = ctor;
                _activator = Instantiator.GetInstanceActivator(ctor, factory);
                var parameters = ConstructorInfo.GetParameters();

                if(parameters.Length == 1 && parameters.First().ParameterType == typeof(PropertyBag))
                {
                    _populators.Add((pb)=> pb);
                } 
                else 
                {
                    parameters.ForEach(pi => {
                        _populators.Add((pb) => pb[pi.Name.ToPascalCase()]);
                    });
                }

                _factory = factory;
            }

            public ConstructorInfo ConstructorInfo { get; }
            public Type Type => ConstructorInfo.DeclaringType;

            public object Build(PropertyBag propertyBag)
            {
                return _activator(ToCtorArgs(propertyBag));
            }

            object[] ToCtorArgs(PropertyBag propertyBag)
            {
                var args = new object[_populators.Count];
                for(int i = 0; i < _populators.Count; i++)
                {
                    args[i] = _populators[i](propertyBag);
                }
                return args;
            }
        }

        private class Instantiator 
        {
            public delegate object InstanceActivator(params object[] args);

            public static InstanceActivator GetInstanceActivator(ConstructorInfo ctor, IObjectFactory factory)
            {
                var type = ctor.DeclaringType;
                var paramsInfo = ctor.GetParameters();                  

                var param = Expression.Parameter(typeof(object[]), "args");
            
                var argsExp = new Expression[paramsInfo.Length];            
                for (int i = 0; i < paramsInfo.Length; i++)
                {
                    var index = Expression.Constant(i);
                    var paramType = paramsInfo[i].ParameterType;              
                    var paramAccessorExp = Expression.ArrayIndex(param, index);
                    Expression getValueExp = null;  
                    if(paramType.IsAPrimitiveType() || paramType == typeof(PropertyBag))
                    {
                        getValueExp = Expression.Convert(paramAccessorExp, paramType);              

                    }            
                    else 
                    {
                        var factoryConstant = Expression.Constant(factory, typeof(IObjectFactory));
                        var typeConstant = Expression.Constant(paramType, typeof(Type));
                        var buildMethod = typeof(IObjectFactory).GetMethods().Where(m => m.Name == "Build").Single(m => !m.IsGenericMethod);
                        var convertExp = Expression.Convert(paramAccessorExp, typeof(PropertyBag));
                        var callExpr = Expression.Call(factoryConstant, buildMethod, new Expression[]{ typeConstant, convertExp });
                        getValueExp = Expression.Convert(callExpr, paramType); 
                    }
                    argsExp[i] = getValueExp;
                }                  

                var newExp = Expression.New(ctor,argsExp);                  
                var lambda = Expression.Lambda(typeof(InstanceActivator), newExp, param);              
                var compiled = (InstanceActivator)lambda.Compile();
                return compiled;
            }
        }
    }
}