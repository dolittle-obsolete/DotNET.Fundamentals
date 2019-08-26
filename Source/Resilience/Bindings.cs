/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq.Expressions;
using System.Reflection;
using Dolittle.DependencyInversion;
using Dolittle.Reflection;

namespace Dolittle.Resilience
{
    /// <summary>
    /// Represents the bindings for all <see cref="IPolicyFor{T}"/>
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        readonly GetContainer _getContainer;

        /// <summary>
        /// Initializes a new instance of <see cref="Bindings"/>
        /// </summary>
        /// <param name="getContainer"></param>
        public Bindings(GetContainer getContainer)
        {
            _getContainer = getContainer;
        }

        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind(typeof(IPolicyFor<>)).To((BindingContext context) => {
                var policies = _getContainer().Get<IPolicies>();

                Expression<Func<IPolicyFor<object>>> getForExpression = () => policies.GetFor<object>();                
                var methodInfo = getForExpression.GetMethodInfo().GetGenericMethodDefinition();                
                var target = context.Service.GenericTypeArguments[0];
                var genericMethodInfo = methodInfo.MakeGenericMethod(target);
                var result = genericMethodInfo.Invoke(policies, null);
                return result;
            });
        }
    }
}