using Machine.Specifications;

namespace Dolittle.Rules.Specs.for_BrokenRuleReason
{
    public class when_getting_instance_without_args
    {
        const string title = "Some title";
        const string description = "With a description";
        static BrokenRuleReason reason = BrokenRuleReason.Create("2319436d-91c8-43f6-b342-d38906bb0c3f", title, description);

        static BrokenRuleReasonInstance instance;

        Because of = () => instance = reason.NoArgs();

        It should_return_a_broken_rule_reason_instance = () => instance.ShouldNotBeNull();
        It should_have_no_args = () => instance.Arguments.Count.ShouldEqual(0);
        It should_hold_the_title = () => instance.Title.ShouldEqual(title);
        It should_hold_the_description = () => instance.Description.ShouldEqual(description);
    }
}
