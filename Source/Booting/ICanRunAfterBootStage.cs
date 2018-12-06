/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Booting
{
    /// <summary>
    /// Defines the system for performing operations after a specific <see cref="BootStage"/>
    /// </summary>
    public interface ICanRunAfterBootStage<T> : ICanPerformPartOfBootStage<T> where T:IRepresentSettingsForBootStage
    {

    }


}
