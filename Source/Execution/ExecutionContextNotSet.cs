/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Execution
{
    /// <summary>
    /// Exception that gets thrown when <see cref="IExecutionContext"/> is not set
    /// </summary>
    public class ExecutionContextNotSet : Exception {}
}