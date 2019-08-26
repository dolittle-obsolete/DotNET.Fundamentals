/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Types;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Resilience.Specs.for_Policies.when_creating
{
    public class and_there_are_multiple_definers_for_default_policy
    {
        static IInstancesOf<ICanDefineDefaultPolicy> default_policy_definers;
        static Exception result;

        Establish context = () =>
        {
            var firstDefiner = new Mock<ICanDefineDefaultPolicy>();
            var secondDefiner = new Mock<ICanDefineDefaultPolicy>();
            default_policy_definers = new StaticInstancesOf<ICanDefineDefaultPolicy>(
                firstDefiner.Object,
                secondDefiner.Object
            );
        };

        Because of = () => result = Catch.Exception(() => new Policies(default_policy_definers));

        It should_throw_multiple_default_policy_definers_found = () => result.ShouldBeOfExactType<MultipleDefaultPolicyDefinersFound>();
    }
}