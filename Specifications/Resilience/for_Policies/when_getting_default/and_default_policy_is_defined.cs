/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.Resilience.Specs.for_Policies.when_getting_default
{
    public class and_default_policy_is_defined : given.defined_default_policy
    {
        static IPolicy policy;
        Because of = () => policy = policies.Default;

        It should_return_a_policy= () => policy.ShouldBeOfExactType<Policy>();
        It should_pass_the_underlying_policy = () => ((Policy)policy).UnderlyingPolicy.ShouldEqual(underlying_policy);

    }
}