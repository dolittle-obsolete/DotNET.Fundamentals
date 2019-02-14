using System.Collections.Generic;
using System.IO;
using System.Linq;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.IO.Tenants.for_TenantAwareFileSystem
{
    public class when_getting_directory_content : given.a_tenant_aware_file_system
    {
        const string path = "some/place/";
        static string[] paths = new string[] {
            "first",
            "second"
        };
        static string[] results;

        static IEnumerable<string> result;

        Establish context = () => 
        {
            results = paths.Select(_ => MapPath(_)).ToArray();
            file_system.Setup(_ => _.GetDirectoriesIn(MapPath(path))).Returns(results);
        };

        Because of = () => result = tenant_aware_file_system.GetDirectoriesIn(path);

        It should_delegate_to_underlying_file_system_and_return_relative_versions = () => result.ShouldEqual(paths);
    }
}