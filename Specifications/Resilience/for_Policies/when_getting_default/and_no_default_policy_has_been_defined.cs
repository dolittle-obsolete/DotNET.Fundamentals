/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.Resilience.Specs.for_Policies.when_getting_default
{
    public class and_no_default_policy_has_been_defined : given.no_default_policy
    {
        static IPolicy policy;

        Because of = () => policy = policies.Default;

        It should_return_a_null_policy = () => policy.ShouldBeOfExactType<PassThroughPolicy>();
    }
}