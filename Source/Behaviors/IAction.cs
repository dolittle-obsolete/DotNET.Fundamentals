/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Assemblies
{
    /// <summary>
    /// Defines the basics of an action that gets performed as the consequence of a <see cref="ITrigger"/> or
    /// part of a <see cref="IBehavior"/>
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Performs the action
        /// </summary>
        void Perform();

    }
}