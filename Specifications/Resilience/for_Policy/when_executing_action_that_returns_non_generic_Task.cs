// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Resilience.Specs.for_Policy
{
    public class when_executing_action_that_returns_non_generic_Task
    {
        static Policy policy;
        static Exception exception;

        Establish context = () => policy = new Policy(Polly.Policy.NoOp());

        Because of = () => exception = Catch.Exception(() => policy.Execute(async () => await Task.CompletedTask.ConfigureAwait(false)));

        It should_fail_because_action_returns_a_task = () => exception.ShouldBeAssignableTo<SynchronousPolicyCannotReturnTask>();
    }
}