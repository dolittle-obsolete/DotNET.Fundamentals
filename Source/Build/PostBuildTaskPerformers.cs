/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Lifecycle;
using Dolittle.Types;

namespace Dolittle.Build
{
    /// <summary>
    /// Represents an implementation of <see cref="IPostBuildTaskPerformers"/>
    /// </summary>
    [Singleton]
    public class PostBuildTaskPerformers : IPostBuildTaskPerformers
    {
        readonly IDictionary<Type, object> _performers;
        readonly IBuildMessages _buildMessages;
        private readonly IPerformerConfigurationLoader _configurationLoader;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeFinder"></param>
        /// <param name="container"></param>
        /// <param name="configurationLoader"></param>
        /// <param name="buildMessages"></param>
        public PostBuildTaskPerformers(
            ITypeFinder typeFinder,
            IContainer container,
            IPerformerConfigurationLoader configurationLoader,
            IBuildMessages buildMessages)
        {
            var performerTypes = typeFinder.FindMultiple(typeof(ICanPerformPostBuildTasks<>));
            _performers = performerTypes.ToDictionary(_ => {
                var @interface = _.GetInterfaces().Single(__ => __.Name.StartsWith(typeof(ICanPerformPostBuildTasks<>).Name));
                var type = @interface.GetGenericArguments()[0];
                return type;
            }, _ => container.Get(_));
            _buildMessages = buildMessages;
            _configurationLoader = configurationLoader;
        }

        /// <inheritdoc/>
        public void Perform()
        {
            _performers.ForEach(_ =>
            {
                var pluginType = _.Value.GetType();

                var pluginTypeName = $"{pluginType.Namespace}.{pluginType.Name}";

                var configuration = _configurationLoader.GetFor(_.Key, pluginTypeName);

                _buildMessages.Information($"Performing post build task '{_.GetType().AssemblyQualifiedName}'");
                var method = pluginType.GetMethod("Perform", BindingFlags.Public | BindingFlags.Instance);
                method.Invoke(_.Value, new object[] { configuration });
            });
        }
    }
}