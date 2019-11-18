/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Rules.for_BrokenRule
{
    public class when_creating
    {
        static Mock<IRule>  rule;
        static Mock<IRuleContext> rule_context;
        static object instance;
        static BrokenRule broken_rule;

        Establish context = () => 
        {
            rule = new Mock<IRule>();
            rule_context = new Mock<IRuleContext>();
            instance = new object();
        };

        Because of = () => broken_rule = new BrokenRule(rule.Object, instance, rule_context.Object);

        It should_hold_the_rule = () => broken_rule.Rule.ShouldEqual(rule.Object);
        It should_hold_the_instance = () => broken_rule.Instance.ShouldEqual(instance);
        It should_hold_the_context = () => broken_rule.Context.ShouldEqual(rule_context.Object);
        It should_have_no_broken_rule_reason_instances = () => broken_rule.Reasons.ShouldBeEmpty();
    }
}