/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Assemblies
{
    /// <summary>
    /// Defines the basics mechanism of a trigger
    /// </summary>
    public interface ITrigger
    {
        /// <summary>
        /// The event that gets called when the trigger triggers
        /// </summary>
        event Triggered Triggered;
    }
}