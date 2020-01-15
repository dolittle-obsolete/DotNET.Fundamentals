// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Types.Testing;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Resilience.Specs.for_Policies.when_getting_typed
{
    public class and_policy_for_type_is_defined
    {
        static Type type = typeof(string);
        static PolicyFor<string> typed_policy;
        static Polly.Policy underlying_policy;

        static Policies policies;

        static Mock<IDefinePolicyForType> typed_policy_definer;

        Establish context = () =>
        {
            underlying_policy = Polly.Policy.NoOp();
            typed_policy_definer = new Mock<IDefinePolicyForType>();
            typed_policy_definer.SetupGet(_ => _.Type).Returns(type);
            typed_policy_definer.Setup(_ => _.Define()).Returns(underlying_policy);

            policies = new Policies(
                new StaticInstancesOf<IDefineDefaultPolicy>(),
                new StaticInstancesOf<IDefineNamedPolicy>(),
                new StaticInstancesOf<IDefinePolicyForType>(typed_policy_definer.Object));
        };

        Because of = () => typed_policy = policies.GetFor<string>() as PolicyFor<string>;

        It should_return_a_policy = () => typed_policy.ShouldNotBeNull();
        It should_pass_the_underlying_policy = () => typed_policy.UnderlyingPolicy.ShouldEqual(underlying_policy);
    }
}