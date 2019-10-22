/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.Resilience.Specs.for_Policies.when_getting_named
{
    public class and_no_policy_for_name_has_been_defined : given.defined_default_policy
    {
        static NamedPolicy policy;

        Because of = () => policy = policies.GetNamed("FourtyTwo") as NamedPolicy;

        It should_return_policy_that_delegates_to_default_policy = () => policy.DelegatedPolicy.ShouldEqual(policies.Default);
    }
}