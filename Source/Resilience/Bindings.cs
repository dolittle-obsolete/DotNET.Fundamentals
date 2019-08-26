/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq.Expressions;
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
                var methodInfo = getForExpression.GetMethodInfo();
                var genericMethodInfo = methodInfo.MakeGenericMethod(context.Service.GenericTypeArguments[0]);
                var result = genericMethodInfo.Invoke(policies, new object[0]);
                return result;
            });
        }
    }
}