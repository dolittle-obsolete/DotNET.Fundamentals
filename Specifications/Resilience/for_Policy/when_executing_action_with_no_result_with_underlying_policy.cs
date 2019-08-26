/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Resilience.Specs.for_Policy
{
    public class when_executing_action_with_no_result_with_underlying_policy
    {
        static Policy policy;
        static bool called;

        Establish context = () => 
        {
            policy = new Policy(Polly.Policy.NoOp());
        };

        Because of = () => policy.Execute(() => called = true);

        It should_call_the_underlying_policy = () => called.ShouldBeTrue();
    }    
}