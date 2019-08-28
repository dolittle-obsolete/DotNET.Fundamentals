/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Types;
using Dolittle.Types.Testing;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Resilience.Specs.for_Policies.when_creating
{
    public class and_there_are_multiple_definers_for_default_policy
    {
        static IInstancesOf<IDefineDefaultPolicy> default_policy_definers;
        static Exception result;

        Establish context = () =>
        {
            var firstDefiner = new Mock<IDefineDefaultPolicy>();
            var secondDefiner = new Mock<IDefineDefaultPolicy>();
            default_policy_definers = new StaticInstancesOf<IDefineDefaultPolicy>(
                firstDefiner.Object,
                secondDefiner.Object
            );
        };

        Because of = () => result = Catch.Exception(() => new Policies(default_policy_definers, new StaticInstancesOf<IDefineNamedPolicy>(), new StaticInstancesOf<IDefinePolicyForType>()));

        It should_throw_multiple_default_policy_definers_found = () => result.ShouldBeOfExactType<MultipleDefaultPolicyDefinersFound>();
    }
}