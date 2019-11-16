/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Rules
{
    /// <summary>
    /// Represents an implementation of <see cref="IRuleContext"/>
    /// </summary>
    public class RuleContext : IRuleContext
    {
        List<RuleFailed> _callbacks = new List<RuleFailed>();

        /// <summary>
        /// Initializes a new instance of <see cref="RuleContext"/>
        /// </summary>
        /// <param name="target">Target to </param>
        public RuleContext(object target)
        {
            Target = target;
        }

        /// <inheritdoc/>
        public object Target { get; }

        /// <inheritdoc/>
        public void OnFailed(RuleFailed callback)
        {
            _callbacks.Add(callback);
        }

        /// <inheritdoc/>
        public void Fail(IRule rule, object instance, BrokenRuleReason reason)
        {
            _callbacks.ForEach(c => c(rule, instance, reason));
        }
    }
}
