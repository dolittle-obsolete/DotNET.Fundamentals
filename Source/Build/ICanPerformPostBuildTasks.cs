/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Build
{
    /// <summary>
    /// Defines an interface for systems that will perform post build tasks 
    /// </summary>
    public interface ICanPerformPostBuildTasks<TConfig> where TConfig:IHoldConfigurationForPostBuildPerformer
    {
        /// <summary>
        /// Method that gets called for performing the necessary build tasks
        /// </summary>
        void Perform(TConfig config);
    }
}