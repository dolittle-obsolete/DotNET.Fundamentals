using System;
using System.Globalization;
using System.IO;
using Dolittle.Applications;
using Dolittle.Execution;
using Dolittle.IO;
using Dolittle.Security;
using Dolittle.Tenancy;
using Machine.Specifications;
using Moq;

namespace Dolittle.IO.Tenants.for_TenantAwareFileSystem.given
{
    public class a_tenant_aware_file_system
    {
        protected static Application application = Guid.Parse("2f076202-7c0a-45e1-b6bb-218682f4d6a4");
        protected static BoundedContext bounded_context = Guid.Parse("d90df5d0-ed59-478a-98ed-967c1bd06364");
        protected static TenantId tenant = Guid.Parse("e00adddf-2fa5-472d-8d27-1314c6dacf0d");
        protected static ExecutionContext execution_context;
        protected static Mock<IFileSystem> file_system;
        protected static Mock<IExecutionContextManager> execution_context_manager; 
        protected static TenantAwareFileSystem tenant_aware_file_system;
        protected static TenantAwareFileSystemConfiguration configuration;

        Establish context = () =>
        {
            execution_context = new ExecutionContext(
                application,
                bounded_context,
                tenant,
                Execution.Environment.Development,
                CorrelationId.New(),
                Claims.Empty,
                CultureInfo.InvariantCulture
               
            );
            execution_context_manager = new Mock<IExecutionContextManager>();
            execution_context_manager.SetupGet(_ => _.Current).Returns(execution_context);
            file_system = new Mock<IFileSystem>();
            configuration = new TenantAwareFileSystemConfiguration("/some_place/with_all_tenants");
            tenant_aware_file_system = new TenantAwareFileSystem(execution_context_manager.Object, configuration, file_system.Object);
        };

        protected static string MapPath(string relativePath)
        {
            var absolutePath = Path.Combine(configuration.RootPath, execution_context.Tenant.ToString(),relativePath);
            return absolutePath;
        }
    }
}