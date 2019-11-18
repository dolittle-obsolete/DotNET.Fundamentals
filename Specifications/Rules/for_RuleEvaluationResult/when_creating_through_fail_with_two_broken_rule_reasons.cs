/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.Rules.for_RuleEvaluationResult
{
    public class when_creating_through_fail_with_two_broken_rule_reasons
    {
        static BrokenRuleReason first_reason = BrokenRuleReason.Create("ffa1f234-2345-46c4-b4c3-dda1f73f5c03", "First reason");
        static BrokenRuleReason second_reason = BrokenRuleReason.Create("296bac42-025a-4697-824e-d5019cf3f1c8", "Second reason");
        static BrokenRuleReasonInstance first_reason_instance;
        static BrokenRuleReasonInstance second_reason_instance;
        static RuleEvaluationResult result;
        static object instance;

        Establish context = () => 
        {
            instance = new object();
            first_reason_instance = first_reason.NoArgs();
            second_reason_instance = second_reason.NoArgs();
        };

        Because of = () => result = RuleEvaluationResult.Fail(instance, first_reason_instance, second_reason_instance);

        It should_be_considered_failed = () => result.IsSuccess.ShouldBeFalse();
        It should_be_considered_failed_through_implicit_operator = () => (result == true).ShouldBeFalse();
        It should_hold_both_reasons = () => result.Reasons.ShouldContainOnly(first_reason_instance, second_reason_instance);
    }
}