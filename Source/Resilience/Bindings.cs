/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.DependencyInversion;

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
            builder.Bind(typeof(IPolicyFor<>)).To(() => {
                var policies = _getContainer().Get<IPolicies>();
                return null;
            });
        }
    }
}