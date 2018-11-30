/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Execution;
using Dolittle.IO;
using Dolittle.Scheduling;
using Dolittle.Time;

namespace Dolittle.Bootstrapping.Stages
{
    /// <summary>
    /// Represents the settings for <see cref="BootStage.InitialSystem"/> stage
    /// </summary>
    public class InitialSystemSettings : IRepresentSettingsForBootStage
    {
        /// <summary>
        /// Initializes anew instance of <see cref="InitialSystemSettings"/>
        /// </summary>
        public InitialSystemSettings(
            IFileSystem fileSystem, 
            IScheduler scheduler, 
            ISystemClock systemClock, 
            Environment environment)
        {
            this.FileSystem = fileSystem;
            this.Scheduler = scheduler;
            this.SystemClock = systemClock;
            this.Environment = environment;

        }

        /// <summary>
        /// Gets the <see cref="IFileSystem"/> to use
        /// </summary>
        /// <value></value>
        public IFileSystem FileSystem {  get; }

        /// <summary>
        /// Gets the <see cref="IScheduler"/> to use
        /// </summary>
        public IScheduler Scheduler {  get; }

        /// <summary>
        /// Gets the <see cref="ISystemClock"/> to use
        /// </summary>
        public ISystemClock SystemClock {  get; }

        /// <summary>
        /// Gets the <see cref="Environment"/> we're running in
        /// </summary>
        public Environment Environment {  get; }
    }
}