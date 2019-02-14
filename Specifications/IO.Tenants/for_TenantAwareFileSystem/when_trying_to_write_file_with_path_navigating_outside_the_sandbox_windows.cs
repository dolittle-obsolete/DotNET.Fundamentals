using System;
using Dolittle.Execution;
using Machine.Specifications;
using Moq;
using It=Machine.Specifications.It;

namespace Dolittle.IO.Tenants.for_TenantAwareFileSystem
{
    public class when_trying_to_write_file_with_path_navigating_outside_the_sandbox_windows : given.a_tenant_aware_file_system
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => tenant_aware_file_system.WriteAllText("someplace\\..\\..\\somefile.txt",""));

        It should_throw_access_outside_sandbox_denied = () => result.ShouldBeOfExactType<AccessOutsideSandboxDenied>();
    }
}