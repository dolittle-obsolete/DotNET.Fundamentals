/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Moq;
using Dolittle.Types.Testing;

namespace Dolittle.Resilience.Specs.for_Policies.given
{
    public class defined_default_policy
    {
        protected static Policies policies;
        protected static Mock<ICanDefineDefaultPolicy> policy_definer;
        protected static Polly.Policy underlying_policy;

        Establish context = () =>
        {
            policy_definer = new Mock<ICanDefineDefaultPolicy>();
            underlying_policy = Polly.Policy.NoOp();
            policy_definer.Setup(_ => _.Define()).Returns(underlying_policy);
            policies = new Policies(
                new StaticInstancesOf<ICanDefineDefaultPolicy>(policy_definer.Object),
                new StaticInstancesOf<ICanDefineNamedPolicy>(),
                new StaticInstancesOf<ICanDefinePolicyForType>()
            );
        };        
    }
}