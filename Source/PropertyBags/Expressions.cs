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

    /// <summary>
    /// Expression helpers that can be used to in the PropertyBag factories
    /// </summary>
    public static class Expressions 
    {
        /// <summary>
        /// An Expression that can be used to build a complex type using an <see cref="IObjectFactory" />
        /// </summary>
        /// <param name="destinationType">The Type of the instance to be built</param>
        /// <param name="factory">An instance of <see cref="IObjectFactory" /> that can be used to build complex types </param>
        /// <param name="accessorExp">An Expression that accesses the correct property on the Source</param>
        /// <returns></returns>
        public static Expression GetBuildExpression(Type destinationType, IObjectFactory factory, Expression accessorExp)
        {
            var factoryConstant = Expression.Constant(factory, typeof(IObjectFactory));
            var typeConstant = Expression.Constant(destinationType, typeof(Type));
            var buildMethod = typeof(IObjectFactory).GetMethods().Where(m => m.Name == "Build").Single(m => !m.IsGenericMethod);
            var convertExp = Expression.Convert(accessorExp, typeof(PropertyBag));
            var callExpr = Expression.Call(factoryConstant, buildMethod, new Expression[]{ typeConstant, convertExp });
            return Expression.Convert(callExpr, destinationType); 
        }

        /// <summary>
        /// An Expression that can be used to build a <see cref="ConceptAs{T}" />
        /// </summary>
        /// <param name="destinationType">The Type of the <see cref="ConceptAs{T}" /> instance to be built</param>
        /// <param name="accessorExp"></param>
        /// <returns>An Expression that accesses the correct property on the Source</returns>
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