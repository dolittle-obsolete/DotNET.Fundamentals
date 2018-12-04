/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Collections;
using Dolittle.Logging;
using Dolittle.Types;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the <see cref="BootStage.BootProcedures"/> stage of booting
    /// </summary>
    public class BootProcedures : ICanPerformBootStage<BootProceduresSettings>
    {
        /// <inheritdoc/>
        public BootStage BootStage => BootStage.BootProcedures;

        /// <inheritdoc/>
        public void Perform(BootProceduresSettings settings, IBootStageBuilder builder)
        {
            if( settings.Enabled )
            {
                var logger = builder.GetAssociation(WellKnownAssociations.Logger) as ILogger;
                var typeFinder = builder.GetAssociation(WellKnownAssociations.TypeFinder) as ITypeFinder;
                var procedures = typeFinder.FindMultiple<ICanPerformBootProcedure>();

                procedures.ForEach(_ => logger.Information($"BootProcedure : {_.AssemblyQualifiedName}"));


                

                
                Bootstrapper.Start(builder.Container, logger);
            }
        }
    }
}