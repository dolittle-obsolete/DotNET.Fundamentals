/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Dolittle.Execution;
using Dolittle.IO;
using Dolittle.Scheduling;
using Dolittle.Time;
using Dolittle.Logging;
using Microsoft.Extensions.Logging;
using Environment = Dolittle.Execution.Environment;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the settings for <see cref="BootStage.InitialSystem"/> stage
    /// </summary>
    public class InitialSystemSettings : IRepresentSettingsForBootStage
    {
        /// <summary>
        /// Gets the <see cref="IFileSystem"/> to use
        /// </summary>
        public IFileSystem FileSystem {  get; internal set; }

        /// <summary>
        /// Gets the <see cref="IScheduler"/> to use
        /// </summary>
        public IScheduler Scheduler {  get; internal set;  }

        /// <summary>
        /// Gets the <see cref="ISystemClock"/> to use
        /// </summary>
        public ISystemClock SystemClock {  get; internal set;  }

        /// <summary>
        /// Gets the <see cref="IContainer"/> used
        /// </summary>
        public IContainer Container { get; internal set;  }
    }
}