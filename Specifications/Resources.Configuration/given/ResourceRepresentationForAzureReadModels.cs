/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 
using System;
using System.Collections.Generic;

namespace Dolittle.Resources.Configuration.Specs.given
{
    public class ResourceRepresentationForAzureReadModels : IRepresentAResourceType
    {
        IDictionary<Type, Type> _bindings;
        
        /// <inheritdoc/>
        public ResourceType Type => "readModels";
        /// <inheritdoc/>
        public ResourceTypeImplementation ImplementationName => "Azure";
        /// <inheritdoc/>
        public Type ConfigurationObjectType => typeof(ReadModelRepositoryConfiguration);
        /// <inheritdoc/>
        public IDictionary<Type, Type> Bindings {
            get 
            {
                if (_bindings == null) 
                    InitializeBindings();
                
                return _bindings;
           }
        }

        void InitializeBindings()
        {
            _bindings = new Dictionary<Type, Type>();
            _bindings.Add(typeof(IReadModelRepositoryFor<>), typeof(AzureReadModelRepositoryFor<>));
        }
    }
}