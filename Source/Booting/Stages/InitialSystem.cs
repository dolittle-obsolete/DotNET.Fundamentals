/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 using Dolittle.DependencyInversion;
 using Dolittle.Execution;
 using Dolittle.IO;
 using Dolittle.Time;
 using Dolittle.Scheduling;

namespace Dolittle.Bootstrapping.Stages
{

    /// <summary>
    /// Represents the <see cref="BootStage.InitialSystem"/> stage of booting
    /// </summary>
    public class InitialSystem : ICanPerformBootStage<InitialSystemSettings>
    {
        /// <inheritdoc/>
        public BootStage BootStage => BootStage.InitialSystem;

        /// <inheritdoc/>
        public void Perform(InitialSystemSettings settings, IBootStageBuilder builder)
        {
            builder.Bindings.Bind<IFileSystem>().To(settings.FileSystem);
            builder.Bindings.Bind<IScheduler>().To(settings.Scheduler);
            builder.Bindings.Bind<ISystemClock>().To(settings.SystemClock);
            builder.Bindings.Bind<Environment>().To(settings.Environment);
        }
    }
}
