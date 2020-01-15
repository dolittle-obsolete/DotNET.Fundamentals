// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using Dolittle.IO;
using Dolittle.Scheduling;
using Dolittle.Time;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the settings for <see cref="BootStage.InitialSystem"/> stage.
    /// </summary>
    public class InitialSystemSettings : IRepresentSettingsForBootStage
    {
        /// <summary>
        /// Gets the <see cref="IFileSystem"/> to use.
        /// </summary>
        public IFileSystem FileSystem { get; internal set; }

        /// <summary>
        /// Gets the <see cref="IScheduler"/> to use.
        /// </summary>
        public IScheduler Scheduler { get; internal set; }

        /// <summary>
        /// Gets the <see cref="ISystemClock"/> to use.
        /// </summary>
        public ISystemClock SystemClock { get; internal set; }

        /// <summary>
        /// Gets the <see cref="IContainer"/> used.
        /// </summary>
        public IContainer Container { get; internal set; }
    }
}