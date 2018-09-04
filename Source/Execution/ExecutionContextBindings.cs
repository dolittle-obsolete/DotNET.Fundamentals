/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading;
using Dolittle.DependencyInversion;

namespace Dolittle.Execution
{
    /// <summary>
    /// Provides bindings for <see cref="ExecutionContext"/>
    /// </summary>
    public class ExecutionContextBindings : ICanProvideBindings
    {
        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            var executionContextManager = new ExecutionContextManager();
            builder.Bind<IExecutionContextManager>().To(executionContextManager);
            builder.Bind<ExecutionContext>().To(() => executionContextManager.Current);
        }
    }
}
