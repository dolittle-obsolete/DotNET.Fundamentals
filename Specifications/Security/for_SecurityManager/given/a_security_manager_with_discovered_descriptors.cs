using System.Collections.Generic;
using doLittle.DependencyInversion;
using doLittle.Execution;
using doLittle.Security;
using doLittle.Types;
using Machine.Specifications;
using Moq;

namespace doLittle.Security.Specs.for_SecurityManager.given
{
    public class a_security_manager_with_discovered_descriptors
    {
        protected static Mock<IInstancesOf<ISecurityDescriptor>> security_descriptors;
        protected static SecurityManager security_manager;

        protected static Mock<ISecurityDescriptor> first_security_descriptor;
        protected static Mock<ISecurityDescriptor> second_security_descriptor;

        Establish context = () =>
        {
            first_security_descriptor = new Mock<ISecurityDescriptor>();
            second_security_descriptor = new Mock<ISecurityDescriptor>();

            security_descriptors = new Mock<IInstancesOf<ISecurityDescriptor>>();
            security_descriptors.Setup(_ => _.GetEnumerator()).Returns(new List<ISecurityDescriptor>(new[]
            {
                first_security_descriptor.Object,
                second_security_descriptor.Object
            }).GetEnumerator());
            
            security_manager = new SecurityManager(security_descriptors.Object);
        };
    }
}
