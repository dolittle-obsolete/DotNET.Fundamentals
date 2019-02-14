using System.IO;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.IO.Tenants.for_TenantAwareFileSystem
{
    public class when_writing_a_valid_file : given.a_tenant_aware_file_system
    {
        const string path = "some/place/file.txt";
        const string content = "Some result";

        Because of = () => tenant_aware_file_system.WriteAllText(path,content);

        It should_delegate_to_underlying_file_system_and_pass_content_along = () => file_system.Verify(_ => _.WriteAllText(MapPath(path), content), Moq.Times.Once());
    }
}