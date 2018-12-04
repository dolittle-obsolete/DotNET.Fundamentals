/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Reflection;
using Dolittle.Assemblies;
using Dolittle.DependencyInversion;
using Dolittle.IO;
using Dolittle.Logging;
using Dolittle.Scheduling;
using Dolittle.Types;

namespace Dolittle.Booting
{
    /// <summary>
    /// Represents well known associations that will be available past the initial system boot stages
    /// </summary>
    public class WellKnownAssociations
    {
        /// <summary>
        /// The entry <see cref="Assembly"/> defined
        /// </summary>
        public const string EntryAssembly = "EntryAssembly";

        /// <summary>
        /// The <see cref="IScheduler">scheduler</see> to be used
        /// </summary>
        public const string Scheduler = "Scheduler";

        /// <summary>
        /// The <see cref="IAssemblies">assemblies</see> available
        /// </summary>
        public const string Assemblies = "Assemblies";

        /// <summary>
        /// The <see cref="ITypeFinder"/>
        /// </summary>
        public const string TypeFinder = "TypeFinder";

        /// <summary>
        /// Which <see cref="Dolittle.Execution.Environment"/> we're running in
        /// </summary>
        public const string Environment = "Environment";

        /// <summary>
        /// The <see cref="ILogger"/> 
        /// </summary>
        public const string Logger = "Logger";

        /// <summary>
        /// Current <see cref="IBindingCollection">bindings</see>
        /// </summary>
        public const string Bindings = "Bindings";
    }
}