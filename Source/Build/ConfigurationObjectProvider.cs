// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Collections;
using Dolittle.Configuration;
using Dolittle.DependencyInversion;
using Dolittle.Reflection;
using Dolittle.Types;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanProvideConfigurationObjects"/> that is capable
    /// of providing configuration objects for <see cref="ICanPerformBuildTask"/>
    /// </summary>
    public class ConfigurationObjectProvider : ICanProvideConfigurationObjects
    {
        readonly IDictionary<Type, string> _configurationObjectTypes = new Dictionary<Type, string>();
        readonly ITypeFinder _typeFinder;
        readonly GetContainer _getContainer;

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationObjectProvider"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for finding performers</param>
        /// <param name="getContainer"><see cref="GetContainer"/> for getting container</param>
        public ConfigurationObjectProvider(
            ITypeFinder typeFinder,
            GetContainer getContainer)
        {
            _typeFinder = typeFinder;
            _getContainer = getContainer;
            PopulateConfigurationObjectTypes();
        }

        /// <inheritdoc/>
        public bool CanProvide(Type type)
        {
            return _configurationObjectTypes.ContainsKey(type);
        }

        /// <inheritdoc/>
        public object Provide(Type type)
        {
            var performerConfigurationManager = _getContainer().Get<IPerformerConfigurationManager>();
            var pluginTypeName = _configurationObjectTypes[type];
            var configObject = performerConfigurationManager.GetFor(type, pluginTypeName);
            return configObject;
        }

        void PopulateConfigurationObjectTypes()
        {
            var performerTypes = _typeFinder.FindMultiple<ICanPerformBuildTask>();
            performerTypes.ForEach(_ =>
            {
                var constructors = _.GetConstructors();
                ThrowIfMoreThanOneConstructors(_, constructors);

                var pluginTypeName = $"{_.Namespace}.{_.Name}";

                if (constructors.Length == 1)
                {
                    var configurationObjectTypes = constructors[0]
                        .GetParameters()
                        .Where(parameter => parameter.ParameterType.HasInterface<IConfigurationObject>())
                        .Select(parameter => parameter.ParameterType)
                        .ToDictionary(type => type, type => pluginTypeName);

                    configurationObjectTypes.ForEach(_configurationObjectTypes.Add);
                }
            });
        }

        void ThrowIfMoreThanOneConstructors(Type _, ConstructorInfo[] constructors)
        {
            if (constructors.Length > 1) throw new AmbiguousConstructor(_);
        }
    }
}