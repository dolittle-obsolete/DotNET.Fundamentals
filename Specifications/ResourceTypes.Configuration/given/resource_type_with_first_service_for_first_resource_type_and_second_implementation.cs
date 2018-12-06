/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 
using System;
using System.Collections.Generic;

namespace Dolittle.ResourceTypes.Configuration.Specs.given
{
    public class resource_type_with_first_service_for_first_resource_type_and_second_implementation : IRepresentAResourceType
    {
        IDictionary<Type, Type> _bindings;
        
        /// <inheritdoc/>
        public ResourceType Type => all_dependencies.first_resource_type;
        /// <inheritdoc/>
        public ResourceTypeImplementation ImplementationName => all_dependencies.second_resource_type_implementation;
        /// <inheritdoc/>
        public Type ConfigurationObjectType => typeof(configuration_for_first_resource_type);
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
            _bindings.Add(typeof(first_service), typeof(implementation_of_first_service_for_second_implementation_type));
        }
    }
}