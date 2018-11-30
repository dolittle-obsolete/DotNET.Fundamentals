/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Represents an implementation of <see cref="IBootBuilder"/>
    /// </summary>
    public class BootBuilder : IBootBuilder
    {
        /// <inheritdoc/>
        public Boot Build()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public void Use<T>(T settings) where T : IRepresentSettingsForBootStage
        {
            throw new System.NotImplementedException();
        }
    }
}
