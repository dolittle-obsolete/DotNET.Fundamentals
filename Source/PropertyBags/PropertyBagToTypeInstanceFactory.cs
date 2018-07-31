namespace Dolittle.PropertyBags
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using Dolittle.Collections;
    using Dolittle.Strings;

    public class PropertyBagToTypeInstanceFactory
    {
        InstanceActivator _activator;
        List<Func<PropertyBag,object>> _parameterAccessors = new List<Func<PropertyBag, object>>();
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
                _parameterAccessors.Add((pb)=> pb);
            } 
            else 
            {
                parameters.ForEach(pi => {
                    _parameterAccessors.Add((pb) => pb[pi.Name.ToPascalCase()]);
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
            var args = new object[_parameterAccessors.Count];
            for(int i = 0; i < _parameterAccessors.Count; i++)
            {
                args[i] = _parameterAccessors[i](propertyBag);
            }
            return args;
        }
    }   
}