/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Logging;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanProvideAssemblies"/> that provides assemblies from the current context
    /// </summary>
    public class DefaultAssemblyProvider : ICanProvideAssemblies
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DefaultAssemblyProvider"/>
        /// </summary>
        /// <param name="logger">Logger for logging</param>
        /// <param name="entryAssembly"><see cref="Assembly">Entry assembly</see> - if null, it will try to get entry assembly</param>
        public DefaultAssemblyProvider(ILogger logger, Assembly entryAssembly = null)
        {
            if( entryAssembly == null ) entryAssembly = Assembly.GetEntryAssembly();
            var dependencyModel = DependencyContext.Load(entryAssembly);

            logger.Information($"Dependency model has {dependencyModel.RuntimeLibraries.Count()} libraries");
            Libraries = dependencyModel.RuntimeLibraries.Cast<RuntimeLibrary>().Where(_ => _.RuntimeAssemblyGroups.Count() > 0).ToArray();
            logger.Information($"Dependency model has {Libraries.Count()} libraries belonging to an assembly group");
            foreach (var library in Libraries) logger.Information($"Providing '{library.Name}, {library.Version}'");
        }

        /// <inheritdoc/>
        public IEnumerable<Library> Libraries { get; }


        /// <inheritdoc/>
        public Assembly GetFrom(Microsoft.Extensions.DependencyModel.Library library)
        {
            return Assembly.Load(new AssemblyName(library.Name));
        }
    }
}