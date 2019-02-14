using System;
using Dolittle.Execution;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.IO.Tenants.for_TenantAwareFileSystem
{
    public class when_trying_to_read_file_with_rooted_path_windows : given.a_tenant_aware_file_system
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => tenant_aware_file_system.ReadAllText("c:\\someplace\\somefile.txt"));

        It should_throw_access_outside_sandbox_denied = () => result.ShouldBeOfExactType<AccessOutsideSandboxDenied>();
    }
}