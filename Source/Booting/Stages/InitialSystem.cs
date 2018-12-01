/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.IO;
using Dolittle.Time;
using Dolittle.Scheduling;

namespace Dolittle.Booting.Stages
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
            var scheduler = settings.Scheduler ?? new AsyncScheduler();

            builder.Associate(WellKnownAssociations.Scheduler, scheduler);

            builder.Bindings.Bind<ISystemClock>().To(settings.SystemClock ?? typeof(SystemClock));;
            builder.Bindings.Bind<IFileSystem>().To(settings.FileSystem ?? typeof(FileSystem));
            builder.Bindings.Bind<IScheduler>().To(scheduler);
            
        }
    }
}
