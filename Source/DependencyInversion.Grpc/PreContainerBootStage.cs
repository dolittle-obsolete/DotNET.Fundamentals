/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Dolittle.Booting;
using Dolittle.DependencyInversion.Booting.Stages;

namespace Dolittle.DependencyInversion.Grpc
{
    /// <summary>
    /// System that gets run before the <see cref="BootStage.Container"/> boot stage
    /// </summary>
    public class PreContainerBootStage : ICanRunBeforeBootStage<ContainerSettings>
    {
        /// <inheritdoc/>
        public BootStage BootStage => BootStage.Container;

        /// <inheritdoc/>
        public void Perform(ContainerSettings settings, IBootStageBuilder builder)
        {
            var bindings = builder.GetAssociation(WellKnownAssociations.Bindings) as IBindingCollection;
            var allBindings = new BindingCollection(bindings.ToArray());
            builder.Bindings.Bind<BindingCollection>().To(allBindings);
        }
    }
}