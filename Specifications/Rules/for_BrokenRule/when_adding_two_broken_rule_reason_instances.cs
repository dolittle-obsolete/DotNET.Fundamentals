/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Rules.for_BrokenRule
{
    public class when_adding_two_broken_rule_reason_instances
    {
        static Mock<IRule>  rule;
        static Mock<IRuleContext> rule_context;
        static object instance;
        static BrokenRule broken_rule;
        static BrokenRuleReason first_reason = BrokenRuleReason.Create("2da82a45-e779-4823-ae12-c02023ee8e5f", "First reason");
        static BrokenRuleReason second_reason = BrokenRuleReason.Create("5e536622-5b92-44ea-ba83-cfa14d5029b4", "Second reason");

        static BrokenRuleReasonInstance first_reason_instance;
        static BrokenRuleReasonInstance second_reason_instance;

        Establish context = () => 
        {
            rule = new Mock<IRule>();
            rule_context = new Mock<IRuleContext>();
            instance = new object();
            broken_rule = new BrokenRule(rule.Object, instance, rule_context.Object);

            first_reason_instance = first_reason.NoArgs();
            second_reason_instance = second_reason.NoArgs();
        };

        Because of = () => 
        {
            broken_rule.AddReason(first_reason_instance);
            broken_rule.AddReason(second_reason_instance);
        };

        It should_contain_only_the_added_reasons = () => broken_rule.Reasons.ShouldContainOnly(first_reason_instance, second_reason_instance);
    }    
}