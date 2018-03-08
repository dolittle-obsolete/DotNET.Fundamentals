using Dolittle.Security;
using Machine.Specifications;

namespace Dolittle.Security.Specs.for_SecurityDescriptor
{
    public class when_configuring
    {
        static SecurityDescriptor   descriptor;

        Because of = () => descriptor = new SecurityDescriptor();

        It should_have_a_when_clause_set = () => descriptor.When.ShouldNotBeNull();
    }
}
