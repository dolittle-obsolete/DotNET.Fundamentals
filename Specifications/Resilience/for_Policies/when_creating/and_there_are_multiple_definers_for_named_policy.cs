// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Types;
using Dolittle.Types.Testing;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Resilience.Specs.for_Policies.when_creating
{
    public class and_there_are_multiple_definers_for_named_policy
    {
        const string policy_name = "Fourty Two";
        static IInstancesOf<IDefineNamedPolicy> named_policy_definers;
        static Exception result;

        Establish context = () =>
        {
            var firstDefiner = new Mock<IDefineNamedPolicy>();
            firstDefiner.SetupGet(_ => _.Name).Returns(policy_name);
            var secondDefiner = new Mock<IDefineNamedPolicy>();
            secondDefiner.SetupGet(_ => _.Name).Returns(policy_name);
            named_policy_definers = new StaticInstancesOf<IDefineNamedPolicy>(
                firstDefiner.Object,
                secondDefiner.Object);
        };

        Because of = () => result = Catch.Exception(() => new Policies(
            new StaticInstancesOf<IDefineDefaultPolicy>(),
            named_policy_definers,
            new StaticInstancesOf<IDefinePolicyForType>()));

        It should_throw_multiple_policy_definers_for_name_found = () => result.ShouldBeOfExactType<MultiplePolicyDefinersForNameFound>();
    }
}