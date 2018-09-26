/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Execution
{
    /// <summary>
    /// The exception that gets thrown when the execution context has initially been set already
    /// </summary>
    public class InitialExecutionContextHasAlreadyBeenSet : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InitialExecutionContextHasAlreadyBeenSet"/>
        /// </summary>
        public InitialExecutionContextHasAlreadyBeenSet() : base("Initial execution context has already been set - it can't set twice in the same process") {}
    }
}