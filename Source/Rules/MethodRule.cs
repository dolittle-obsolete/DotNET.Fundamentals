/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Collections;

namespace Dolittle.Rules
{
    /// <summary>
    /// Represents a <see cref="IRule"/> that is implemented as a method
    /// </summary>
    public class MethodRule : IRule
    {
        readonly Func<RuleEvaluationResult> _method;

        /// <summary>
        /// Initializes a new instance of <see cref="MethodRule"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="method"></param>
        public MethodRule(string name, Func<RuleEvaluationResult> method)
        {
            Name = name;
            _method = method;
        }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public void Evaluate(IRuleContext context, object instance)
        {
            var result = _method();
            if (!result.IsSuccess)
            {
                result.Causes.ForEach(_ => context.Fail(this, instance, _));
            }
        }
    }
}
