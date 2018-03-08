/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Collections;
using Dolittle.Types;

namespace Dolittle.Resources
{
    /// <summary>
    /// Represents an implementation of <see cref="IResourceDefinitions"/>
    /// </summary>
    public class ResourceDefinitions : IResourceDefinitions
    {
        readonly List<IResourceDefinition> _definitions = new List<IResourceDefinition>();

        /// <summary>
        /// Initializes a new instance of <see cref="ResourceDefinitions"/>
        /// </summary>
        /// <param name="resourceDefiners">Instances of <see cref="ICanDefineResource"/></param>
        /// <param name="builderFactory"><see cref="IResourceDefinitionBuilderFactory"/> for creating <see cref="IResourceDefinitionBuilder"/></param>
        public ResourceDefinitions(
            IInstancesOf<ICanDefineResource> resourceDefiners,
            IResourceDefinitionBuilderFactory builderFactory)
        {
            PopulateDefinitions(resourceDefiners, builderFactory);
        }

        /// <inheritdoc/>
        public IEnumerable<IResourceDefinition> All => _definitions;
        void PopulateDefinitions(IInstancesOf<ICanDefineResource> resourceDefiners, IResourceDefinitionBuilderFactory builderFactory)
        {
            resourceDefiners.ForEach(definer =>
            {
                var builder = builderFactory.Create();
                definer.Define(builder);

                var definition = builder.Build();
                ThrowIfDefinitionAlreadyExists(definition);
                _definitions.Add(definition);
            });
        }

        void ThrowIfDefinitionAlreadyExists(IResourceDefinition definition)
        {
            if( _definitions.Any(_ => _.Name == definition.Name) ) 
                throw new MultipleResourcesWithSameName(definition.Name);
        }
    }
}