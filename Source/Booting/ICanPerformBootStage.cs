/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Defines the system for performing a specific <see cref="BootStage"/>
    /// </summary>
    public interface ICanPerformBootStage<T> : ICanPerformPartOfBootStage<T> where T:IRepresentSettingsForBootStage
    {

    }
}