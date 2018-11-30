/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Lifecycle;
using Dolittle.Types;
using Dolittle.Collections;
using Dolittle.Reflection;
using Dolittle.Logging;

namespace Dolittle.ResourceTypes.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IResourceConfiguration"/>
    /// </summary>
    [Singleton]
    public class ResourceConfiguration : IResourceConfiguration
    {
        readonly ITypeFinder _typeFinder;
        readonly ILogger _logger;
        readonly IEnumerable<IRepresentAResourceType> _resourceTypeRepresentations;
        readonly IDictionary<ResourceType, ResourceTypeImplementation> _resources = new Dictionary<ResourceType, ResourceTypeImplementation>();

        /// <summary>
        /// Initializes a new instance of <see cref="ResourceConfiguration"/>
        /// </summary>
        /// <param name="typeFinder"></param>
        /// <param name="logger"></param>
        public ResourceConfiguration(ITypeFinder typeFinder, ILogger logger)
        {
            logger.Information("ResourceConfiguration() - ctor");

            _typeFinder = typeFinder;
            var resourceTypeRepresentationTypes = _typeFinder.FindMultiple<IRepresentAResourceType>();
            resourceTypeRepresentationTypes.ForEach(_ => 
            {
                logger.Trace($"Discovered resource type representation : '{_.AssemblyQualifiedName}'");
                ThrowIfNoDefaultConstructor(_);
            });

            _resourceTypeRepresentations = resourceTypeRepresentationTypes.Select(_ => Activator.CreateInstance(_) as IRepresentAResourceType);
            ThrowIfMultipleResourcesWithSameTypeAndImplementation(_resourceTypeRepresentations);
            _logger = logger;
        }

        /// <inheritdoc/>
        public Type GetImplementationFor(Type service)
        {
            _logger.Trace($"Get implementation for {service.AssemblyQualifiedName}");
            var resourceTypesRepresentationsWithService = _resourceTypeRepresentations.Where(_ => _.Bindings.ContainsKey(service));

            resourceTypesRepresentationsWithService.ForEach(_ => _logger.Information($"Resource type with service binding : {_.ImplementationName}"));

            _logger.Trace("Current resources");
            _resources.ForEach(_ => _logger.Information($"Resource : {_.Key} - {_.Value}"));

            var results = resourceTypesRepresentationsWithService.Where(_ => {
                var resourceType = _.Type;
                if (! _resources.ContainsKey(resourceType)) return false;
                var resourceTypeImplementation = _.ImplementationName;
                return resourceTypeImplementation == _resources[resourceType];
            }).ToArray();
            var length = results.Length;
            if (length == 0) throw new ImplementationForServiceNotFound(service);
            if (length > 1) throw new MultipleImplementationsFoundForService(service);

            return results[0].Bindings[service];
        }
        
        /// <inheritdoc/>
        public void ConfigureResourceTypes(IDictionary<ResourceType, ResourceTypeImplementation> resourceTypeToImplementationMap)
        {
            _logger.Information($"Resource Types Configured : {resourceTypeToImplementationMap}");
            resourceTypeToImplementationMap.ForEach(_ => 
            {
                _logger.Information($"Adding resource type '{_.Key}' with implementation {_.Value}");
                _resources[_.Key] = _.Value;
            });
            IsConfigured = true;
        }

        /// <inherit/>
        public bool IsConfigured {get; private set;}

        
        void ThrowIfNoDefaultConstructor(Type resourceTypeRepresentationType)
        {
            if (! resourceTypeRepresentationType.HasDefaultConstructor()) throw new InvalidResourceTypeFound($"The ResourceType representation {resourceTypeRepresentationType.FullName} must have default constructor.");
        }

        void ThrowIfMultipleResourcesWithSameTypeAndImplementation(IEnumerable<IRepresentAResourceType> resourceTypeRepresentations)
        {
            var resourcesGroupedByResourceType = resourceTypeRepresentations.GroupBy(_ => _.Type);
            resourcesGroupedByResourceType.ForEach(group => 
            {
                var numResources = group.Count();
                if (group.GroupBy(_ => _.ImplementationName).Count() != numResources) throw new FoundDuplicateResourceDefinition(group.Key);
            });
        }
    }
}