using System;
using System.Collections.Generic;
using doLittle.DependencyInversion;
using doLittle.Execution;
using doLittle.Security;
using doLittle.Types;
using Machine.Specifications;
using Moq;

namespace doLittle.Security.Specs.for_SecurityManager.given
{
    public class a_security_manager_with_no_descriptors
    {
        protected static Mock<IInstancesOf<ISecurityDescriptor>> security_descriptors;
        protected static SecurityManager security_manager;

        Establish context = () =>
            {
                security_descriptors = new Mock<IInstancesOf<ISecurityDescriptor>>();
                security_descriptors.Setup(_ => _.GetEnumerator()).Returns(new List<ISecurityDescriptor>().GetEnumerator());
                security_manager = new SecurityManager(security_descriptors.Object);
            };
    }
}