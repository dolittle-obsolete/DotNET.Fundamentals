/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Dolittle.Rules
{
    /// <summary>
    /// Represents the result of a an evaluation of a <see cref="IRule"/>
    /// </summary>
    public class RuleEvaluationResult
    {
        /// <summary>
        /// Gets the value representing a successful evaluation
        /// </summary>
        public static readonly RuleEvaluationResult Success = new RuleEvaluationResult(string.Empty);

        /// <summary>
        /// Create a failed <see cref="RuleEvaluationResult"/> with any <see cref="BrokenRuleReason">reasons</see>
        /// </summary>
        /// <param name="instance">Instance to fail</param>
        /// <param name="reasons">Params of <see cref="BrokenRuleReason">reasons</see> to fail</param>
        /// <returns></returns>
        public static RuleEvaluationResult Fail(object instance, params BrokenRuleReasonInstance[] reasons)
        {
            return new RuleEvaluationResult(instance, reasons);
        }

        /// <summary>
        /// Implicitly convert from <see cref="RuleEvaluationResult"/> to <see cref="bool"/>
        /// </summary>
        /// <param name="result"><see cref="RuleEvaluationResult"/> to convert from</param>
        public static implicit operator bool(RuleEvaluationResult result) => result.IsSuccess;


        /// <summary>
        /// Initializes a new instance of <see cref="RuleEvaluationResult"/>
        /// </summary>
        /// <param name="instance">Instance that was evaluated</param>
        /// <param name="reasons">Params of <see cref="BrokenRuleReason">reasons</see></param>
        public RuleEvaluationResult(object instance, params BrokenRuleReasonInstance[] reasons)
        {
            Instance = instance;
            Reasons = reasons;
        }

        /// <summary>
        /// Get the instance that was evaluated
        /// </summary>
        public object Instance { get; }

        /// <summary>
        /// Get the <see cref="BrokenRuleReason">reasons</see> - if there were anything broken
        /// </summary>
        public IEnumerable<BrokenRuleReasonInstance> Reasons { get; }

        /// <summary>
        /// Gets whether or not the result is success or not - based on any reasons
        /// </summary>
        public bool IsSuccess => !Reasons.Any();
    }
}
