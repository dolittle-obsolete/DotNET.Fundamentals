/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Defines the different stages of booting
    /// </summary>
    public enum BootStage
    {
        /// <summary>
        /// Initial system stage - fixed
        /// </summary>
        /// <remarks>
        /// This stage is defined by the system and can't be swapped out. It also does not support
        /// any <see cref="ICanRunBeforeBootStage{T}"/> or <see cref="ICanRunAfterBootStage{T}"/>
        /// </remarks>
        InitialSystem=1,

        /// <summary>
        /// Discovery stage - fixed
        /// </summary>
        /// <remarks>
        /// This stage is defined by the system and can't be swapped out. It also does not support
        /// any <see cref="ICanRunBeforeBootStage{T}"/> or <see cref="ICanRunAfterBootStage{T}"/>
        /// </remarks>
        Discovery,

        /// <summary>
        /// Start of boot
        /// </summary>
        BootStart,

        /// <summary>
        /// Configuration is hooked up. After this, all systems can start relying on configuration to be there.
        /// </summary>
        Configuration,

        /// <summary>
        /// Main bindings hookup
        /// </summary>
        Bindings,

        /// <summary>
        /// Main running of <see cref="ICanPerformBootProcedure">boot procedures</see>
        /// </summary>
        BootProcedures,
        
        /// <summary>
        /// The application stage - which typically means that the booting is over
        /// </summary>
        Application
    }
}
