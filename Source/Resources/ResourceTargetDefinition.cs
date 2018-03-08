/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Collections;

namespace Dolittle.Resources
{
    /// <summary>
    /// Represents an implementation of <see cref="IResourceTargetDefinition"/>
    /// </summary>
    public class ResourceTargetDefinition : IResourceTargetDefinition
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ResourceTargetDefinition"/>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="targets"></param>
        public ResourceTargetDefinition(
            IResourceDefinition source,
            IEnumerable<ResourceServiceTarget> targets)
        {
            ThrowIfAmbiguousServiceTargets(source, targets);
            Source = source;
            Targets = targets;
        }


        /// <inheritdoc/>
        public IResourceDefinition Source { get; }

        /// <inheritdoc/>
        public IEnumerable<ResourceServiceTarget> Targets { get; }

        /// <inheritdoc/>
        public ResourceServiceTarget GetServiceTarget(string name)
        {
            ThrowIfMissingServiceTarget(name);
            return Targets.Single(target => target.Name == name);
        }

        /// <inheritdoc/>
        public bool HasServiceTarget(string name)
        {
            return Targets.Any(target => target.Name == name);
        }

        void ThrowIfAmbiguousServiceTargets(IResourceDefinition source, IEnumerable<ResourceServiceTarget> targets)
        {
            targets.GroupBy(target => target.Service.Service).ForEach(groupedByService => {
                groupedByService.GroupBy(target => target.Name).ForEach(groupedByTargetName => {
                    if( groupedByTargetName.Count() > 1 ) throw new AmbiguousServiceTargets(source, groupedByTargetName.Key);
                });
            });
        }

        void ThrowIfMissingServiceTarget(string name)
        {
            if (!HasServiceTarget(name)) throw new MissingServiceTarget(Source, name);
        }
       
    }
}