/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace Dolittle.Resources
{
    /// <summary>
    /// Represents an implementation of <see cref="IResourceDefinitionBuilder"/>
    /// </summary>
    public class ResourceDefinitionBuilder : IResourceDefinitionBuilder
    {
        string _name = null;
        List<ResourceService> _services = new List<ResourceService>();


        /// <inheritdoc/>
        public IResourceDefinition Build()
        {
            ThrowWhenMissingName();
            ThrowWhenNoServicesDefined();

            var resourceDefinition = new ResourceDefinition(_name, _services);
            return resourceDefinition;
        }

        /// <inheritdoc/>
        public IResourceDefinitionBuilder Requires<T>()
        {
            _services.Add(new ResourceService(typeof(T)));
            return this;
        }


        /// <inheritdoc/>
        public IResourceDefinitionBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        void ThrowWhenMissingName()
        {
            if (_name == null) throw new MissingNameFromResourceDefinition();
        }

        void ThrowWhenNoServicesDefined()
        {
            if (_services.Count == 0) throw new NoResourceServicesDefined(_name);
        }
    }
}