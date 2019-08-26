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
    public class and_there_are_multiple_definers_for_typed_policy
    {
        static Type policy_type = typeof(string);
        static IInstancesOf<ICanDefinePolicyForType> typed_policy_definers;
        static Exception result;

        Establish context = () =>
        {
            var firstDefiner = new Mock<ICanDefinePolicyForType>();
            firstDefiner.SetupGet(_ => _.Type).Returns(policy_type);
            var secondDefiner = new Mock<ICanDefinePolicyForType>();
            secondDefiner.SetupGet(_ => _.Type).Returns(policy_type);
            typed_policy_definers = new StaticInstancesOf<ICanDefinePolicyForType>(
                firstDefiner.Object,
                secondDefiner.Object
            );
        };

        Because of = () => result = Catch.Exception(() => new Policies(
            new StaticInstancesOf<ICanDefineDefaultPolicy>(), 
            new StaticInstancesOf<ICanDefineNamedPolicy>(),
            typed_policy_definers
        ));

        It should_throw_multiple_policy_definers_for_type_found = () => result.ShouldBeOfExactType<MultiplePolicyDefinersForTypeFound>();
    }
}