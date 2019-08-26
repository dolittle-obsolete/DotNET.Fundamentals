/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Resilience.Specs.for_Policy
{
    public class when_executing_action_with_result_with_underlying_policy
    {
        const string expected_result = "Fourty Two";
        static Policy policy;
        static string result;

        Establish context = () => 
        {
            policy = new Policy(Polly.Policy.NoOp());
        };

        Because of = () => result = policy.Execute(() => expected_result);

        It should_forward_call_to_delegated_policy_and_return_result = () => result.ShouldEqual(expected_result);
    }

}