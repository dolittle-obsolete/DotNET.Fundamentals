/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Diagnostics;

namespace Dolittle.Resilience
{
    /// <summary>
    /// Defines the basics for a policy
    /// </summary>
    public interface IPolicy
    {
        /// <summary>
        /// Execute an action within the policy
        /// </summary>
        /// <param name="action"><see cref="Action"/> to execute</param>
        [DebuggerStepThrough]
        void Execute(Action action);

        /// <summary>
        /// Executes an action within the policy and returns the result from the action
        /// </summary>
        /// <param name="action"><see cref="Func{TResult}"/> to call</param>
        /// <returns>Result from the action</returns>
        [DebuggerStepThrough]
        TResult Execute<TResult>(Func<TResult> action);        
    }
}