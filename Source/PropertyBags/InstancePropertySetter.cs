namespace Dolittle.PropertyBags
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Dolittle.Reflection;
    using Dolittle.Collections;
    using Dolittle.Concepts;

    public class InstancePropertySetter
    {
        List<Func<PropertyBag,object>> _accessors = new List<Func<PropertyBag, object>>();
        List<Action<object,object>> _setters = new List<Action<object, object>>();

        public InstancePropertySetter(Type type, IObjectFactory factory )
        {
            Type = type;
            var props = Type.GetSettableProperties();

            props.ForEach(pi => {
                _setters.Add(Actions.GetPropertySetter(type,pi));
                _accessors.Add((pb) => 
                {
                    var value = pb[pi.Name];
                    if(value == null)
                        return value;
                    if(pi.PropertyType.IsAPrimitiveType() || pi.PropertyType == typeof(PropertyBag))
                        return value;

                    if(pi.PropertyType.IsConcept())
                        return ConceptFactory.CreateConceptInstance(pi.PropertyType,value);

                    return factory.Build(pi.PropertyType,value as PropertyBag);
                });
            });
        }
        public Type Type { get; }

        public void Populate(object instance, PropertyBag propertyBag)
        {
            for(int i = 0; i < _setters.Count(); i++)
            {
                _setters[i](instance,_accessors[i](propertyBag));
            }
        }
    }
}