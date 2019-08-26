/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Types.Testing;
using Machine.Specifications;
using Moq;
using It=Machine.Specifications.It;

namespace Dolittle.Resilience.Specs.for_Policies.when_getting_named
{
    public class and_policy_for_name_is_defined : given.defined_default_policy
    {
        const string name = "Fourty Two";
        static NamedPolicy named_policy;

        static Mock<ICanDefineNamedPolicy> named_policy_definer;

        Establish context = () => 
        {
            underlying_policy = Polly.Policy.NoOp();
            named_policy_definer = new Mock<ICanDefineNamedPolicy>();
            named_policy_definer.SetupGet(_ => _.Name).Returns(name);
            named_policy_definer.Setup(_ => _.Define()).Returns(underlying_policy);
            policy_definer.Setup(_ => _.Define()).Returns(underlying_policy);
            
            policies = new Policies(
                new StaticInstancesOf<ICanDefineDefaultPolicy>(policy_definer.Object),
                new StaticInstancesOf<ICanDefineNamedPolicy>(named_policy_definer.Object),
                new StaticInstancesOf<ICanDefinePolicyForType>()
            );
        };

        Because of = () => named_policy = policies.GetNamed(name) as NamedPolicy;

        It should_return_a_policy = () => named_policy.ShouldNotBeNull();
        It should_pass_the_underlying_policy = () => named_policy.UnderlyingPolicy.ShouldEqual(underlying_policy);
    }
}