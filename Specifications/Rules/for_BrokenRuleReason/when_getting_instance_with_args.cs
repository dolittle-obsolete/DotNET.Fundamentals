using Machine.Specifications;

namespace Dolittle.Rules.Specs.for_BrokenRuleReason
{
    public class when_getting_instance_with_args
    {
        const string title = "Some title";
        const string description = "With a description";
        const string first_argument = "Fourty Two";
        const int second_argument = 42;
        static BrokenRuleReason reason = BrokenRuleReason.Create("2319436d-91c8-43f6-b342-d38906bb0c3f", title, description);

        static object args;

        static BrokenRuleReasonInstance instance;

        Establish context = () => args = new{FirstArgument=first_argument, SecondArgument=second_argument};

        Because of = () => instance = reason.WithArgs(args);

        It should_return_a_broken_rule_reason_instance = () => instance.ShouldNotBeNull();
        It should_hold_the_first_argument_in_the_instance = () => instance.Arguments["FirstArgument"].ShouldEqual(first_argument);
        It should_hold_the_second_argument_in_the_instance = () => instance.Arguments["SecondArgument"].ShouldEqual(second_argument.ToString());

        It should_hold_the_title = () => instance.Title.ShouldEqual(title);
        It should_hold_the_description = () => instance.Description.ShouldEqual(description);
    }
}
