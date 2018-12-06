/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Booting;
using Dolittle.Types;

namespace Dolittle.DependencyInversion.Booting.Stages
{
    /// <summary>
    /// Represents the <see cref="BootStage.PrepareBoot"/> stage of booting
    /// </summary>
    public class PrepareBoot : ICanPerformBootStage<NoSettings>
    {
        /// <inheritdoc/>
        public BootStage BootStage => BootStage.PrepareBoot;

        /// <inheritdoc/>
        public void Perform(NoSettings settings, IBootStageBuilder builder)
        {
            var bindings = builder.GetAssociation(WellKnownAssociations.Bindings) as IBindingCollection;
            var newBindingsNotifier = builder.GetAssociation(WellKnownAssociations.NewBindingsNotificationHub) as ICanNotifyForNewBindings;
            var bootContainer = new BootContainer(bindings, newBindingsNotifier);
            builder.UseContainer(bootContainer);
        }
    }
}