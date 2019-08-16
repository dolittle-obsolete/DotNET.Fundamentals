/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Resilience
{
    /// <summary>
    /// Defines a 
    /// </summary>
    public interface IPolicyFor<T>
    {
        /// <summary>
        /// Execute an action within the policy
        /// </summary>
        /// <param name="action"><see cref="Action"/> to execute</param>
        void Execute(Action action);
    }
}