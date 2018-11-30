/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Defines the system for performing operations before a specific <see cref="BootStage"/>
    /// </summary>
    public interface ICanRunBeforeBootStage<T> : ICanPerformPartOfBootStage<T> where T:IRepresentSettingsForBootStage
    {

    }


}
