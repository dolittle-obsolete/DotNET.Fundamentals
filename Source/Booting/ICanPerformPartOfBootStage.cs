/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Booting
{
    /// <summary>
    /// Defines the basis of a system that is capable of performing operations as part of a <see cref="BootStage"/>
    /// </summary>
    public interface ICanPerformPartOfBootStage 
    {
        /// <summary>
        /// Gets the <see cref="BootStage"/> it supports
        /// </summary>
        BootStage BootStage { get; }
    }

    /// <summary>
    /// Defines a system that is capable of performing operations as part of a <see cref="BootStage"/>
    /// </summary>
    public interface ICanPerformPartOfBootStage<T> : ICanPerformPartOfBootStage
        where T:IRepresentSettingsForBootStage
    {

        /// <summary>
        /// Method that gets called when system wants you to perform operations
        /// </summary>
        /// <param name="settings"><see cref="IRepresentSettingsForBootStage">Settings</see> for the <see cref="BootStage"/></param>
        /// <param name="builder"><see cref="IBootStageBuilder"/> for the <see cref="BootStage"/> you represent</param>
        void Perform(T settings, IBootStageBuilder builder);
    }
}