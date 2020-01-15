// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.IO.Tenants.for_Files
{
    public class when_trying_to_write_file_with_path_up_a_level_in_the_hierarchy_windows : given.a_tenant_aware_file_system
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => tenant_aware_file_system.WriteAllText("..\\someplace\\somefile.txt", ""));

        It should_throw_access_outside_sandbox_denied = () => result.ShouldBeOfExactType<AccessOutsideSandboxDenied>();
    }
}