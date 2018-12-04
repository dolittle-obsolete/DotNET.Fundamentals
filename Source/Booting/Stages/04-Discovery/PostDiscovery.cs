/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Types;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents something that runs post the <see cref="BootStage.Discovery"/> stage of booting
    /// </summary>
    public class PostDiscovery : ICanRunAfterBootStage<DiscoverySettings>
    {
        readonly Action<ITypeFinder> _callback;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        public PostDiscovery(Action<ITypeFinder> callback)
        {
            _callback = callback;
        }

        /// <inheritdoc/>
        public BootStage BootStage => BootStage.Discovery;

        /// <inheritdoc/>
        public void Perform(DiscoverySettings settings, IBootStageBuilder builder)
        {
            var typeFinder = builder.GetAssociation(WellKnownAssociations.TypeFinder) as ITypeFinder;
            _callback(typeFinder);
        }
    }
}