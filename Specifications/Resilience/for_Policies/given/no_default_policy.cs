/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Dolittle.Types.Testing;

namespace Dolittle.Resilience.Specs.for_Policies.given
{
    public class no_default_policy
    {
        protected static Policies policies;

        Establish context = () =>
        {
            policies = new Policies(
                new StaticInstancesOf<IDefineDefaultPolicy>(),
                new StaticInstancesOf<IDefineNamedPolicy>(),
                new StaticInstancesOf<IDefinePolicyForType>()
            );
        };        
    }
}