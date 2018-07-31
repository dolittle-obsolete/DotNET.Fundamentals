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
    using Dolittle.Concepts;

    public static class Expressions 
    {
        public static Expression GetBuildExpresssion(Type destinationType, IObjectFactory factory, Expression accessorExp)
        {
            var factoryConstant = Expression.Constant(factory, typeof(IObjectFactory));
            var typeConstant = Expression.Constant(destinationType, typeof(Type));
            var buildMethod = typeof(IObjectFactory).GetMethods().Where(m => m.Name == "Build").Single(m => !m.IsGenericMethod);
            var convertExp = Expression.Convert(accessorExp, typeof(PropertyBag));
            var callExpr = Expression.Call(factoryConstant, buildMethod, new Expression[]{ typeConstant, convertExp });
            return Expression.Convert(callExpr, destinationType); 
        }

        public static Expression GetConceptExpression(Type destinationType, Expression accessorExp)
        {
            var typeConstant = Expression.Constant(destinationType, typeof(Type));
            var buildMethod = typeof(ConceptFactory).GetMethods().Single(m => m.Name == "CreateConceptInstance");
            var convertExp = Expression.Convert(accessorExp, typeof(object));
            var callExpr = Expression.Call(null, buildMethod, new Expression[]{ typeConstant, convertExp });
            return Expression.Convert(callExpr, destinationType); 
        }
    }
}