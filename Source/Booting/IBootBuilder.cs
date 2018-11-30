/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Defines the builder for <see cref="Boot"/>
    /// </summary>
    public interface IBootBuilder
    {
        /// <summary>
        /// Use a specific setting
        /// </summary>
        /// <param name="settings">Setting to use</param>
        /// <typeparam name="T">Type of setting</typeparam>
        void Use<T>(T settings) where T: IRepresentSettingsForBootStage;

        /// <summary>
        /// Build the <see cref="Boot"/>
        /// </summary>
        /// <returns>Built <see cref="Boot"/></returns>
        Boot Build();
    }
}
