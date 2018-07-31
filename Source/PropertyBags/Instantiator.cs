namespace Dolittle.PropertyBags
{
    using System.Linq.Expressions;
    using System.Reflection;
    using Dolittle.Reflection;
    using Dolittle.Concepts;

    public delegate object InstanceActivator(params object[] args);     

    public static class Instantiator 
    {
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
                var accessorExp = Expression.ArrayIndex(param, index);
                Expression getValueExp = null;  
                if(paramType.IsAPrimitiveType() || paramType == typeof(PropertyBag))
                {
                    getValueExp = Expression.Convert(accessorExp, paramType);              
                }            
                else if(paramType.IsConcept())
                {
                    getValueExp = Expressions.GetConceptExpression(paramType,accessorExp);
                }
                else
                {
                    getValueExp = Expressions.GetBuildExpresssion(paramType,factory,accessorExp);
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