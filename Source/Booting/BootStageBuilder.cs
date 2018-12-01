/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.DependencyInversion;

namespace Dolittle.Booting
{
    /// <summary>
    /// Represents an implementation of <see cref="IBootStageBuilder"/>
    /// </summary>
    public class BootStageBuilder : IBootStageBuilder
    {
        readonly Dictionary<string, object> _associations = new Dictionary<string, object>();
        IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="BootStageBuilder"/>
        /// </summary>
        public BootStageBuilder(IDictionary<string, object> initialAssociations = null)
        {
            if( initialAssociations != null ) _associations = new Dictionary<string, object>(initialAssociations);
            Bindings = new BindingProviderBuilder();
        }

        /// <inheritdoc/>
        public IBindingProviderBuilder Bindings { get; }

        /// <inheritdoc/>
        public void Associate(string key, object value)
        {
            _associations[key] = value;
        }

        /// <inheritdoc/>
        public object GetAssociation(string key)
        {
            return _associations[key];
        }


        /// <inheritdoc/>
        public BootStageResult Build()
        {
            var result = new BootStageResult(_container, Bindings.Build(), _associations);
            return result;
        }


        /// <inheritdoc/>
        public void UseContainer(IContainer container)
        {
            _container = container;
        }
    }
}