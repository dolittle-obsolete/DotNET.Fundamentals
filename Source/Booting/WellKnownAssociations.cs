/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Booting
{
    /// <summary>
    /// Represents well known associations that will be available past the initial system boot stages
    /// </summary>
    public class WellKnownAssociations
    {
        /// <summary>
        /// The entry assembly defined
        /// </summary>
        public const string EntryAssembly = "EntryAssembly";

        /// <summary>
        /// The scheduler to be used
        /// </summary>
        public const string Scheduler = "Scheduler";

        /// <summary>
        /// The assemblies available
        /// </summary>
        public const string Assemblies = "Assemblies";

        /// <summary>
        /// Type finder
        /// </summary>
        public const string TypeFinder = "TypeFinder";

        /// <summary>
        /// Which environment we're running in
        /// </summary>
        public const string Environment = "Environment";

        /// <summary>
        /// The logger
        /// </summary>
        public const string Logger = "Logger";

        /// <summary>
        /// Current Bindings
        /// </summary>
        public const string Bindings = "Bindings";
    }
}