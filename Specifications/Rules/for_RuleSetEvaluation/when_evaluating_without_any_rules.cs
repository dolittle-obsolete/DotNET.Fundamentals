/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.Rules.for_RuleSetEvaluation
{
    public class when_evaluating_without_any_rules
    {
        static RuleSetEvaluation evaluation;

        Establish context = () => evaluation = new RuleSetEvaluation(new RuleSet(new IRule[0]));
        
        Because of = () => evaluation.Evaluate(new object());

        It should_not_have_any_broken_rules = () => evaluation.BrokenRules.ShouldBeEmpty();
        It should_be_considered_successful = () => evaluation.IsSuccess.ShouldBeTrue();
        It should_be_considered_successful_through_implicit_operator = () => (evaluation == true).ShouldBeTrue();
    }
}