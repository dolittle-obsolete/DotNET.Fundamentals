// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Threading;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Tenancy;
using Microsoft.Extensions.Logging;

namespace AssemblyContext
{
    static class Program
    {
        static readonly AsyncLocal<LoggingContext> _currentLoggingContext = new AsyncLocal<LoggingContext>();

        static void Main()
        {
            var assembly = typeof(Program).Assembly;

            var loggerFactory = LoggerFactory.Create(_ => _.AddConsole());
            var logAppender = new DefaultLogAppender(GetCurrentLoggingContext, loggerFactory);
            var logAppenders = new LogAppenders(Array.Empty<ICanConfigureLogAppenders>(), logAppender);
            var logger = new Logger(logAppenders) as Dolittle.Logging.ILogger;

            logger.Information($"Creating assembly context for '{assembly}'");

            var assemblyContext = Dolittle.Assemblies.AssemblyContext.From(assembly);

            var projectReferencedAssemblies = assemblyContext.GetProjectReferencedAssemblies();

            foreach (var projectReferencedAssembly in projectReferencedAssemblies)
            {
                logger.Information($"Project referenced assembly '{projectReferencedAssembly.FullName}'");
            }

            var allExportedTypes = projectReferencedAssemblies.SelectMany(_ => _.ExportedTypes).ToArray();
            var controllerType = allExportedTypes.First(_ => _ == typeof(MyController));

            var referencedAssemblies = assemblyContext.GetReferencedAssemblies();
            logger.Information($"Total referenced assemblies : {referencedAssemblies.Count()}");

            logger.Information($"Totally {allExportedTypes.Length} exported types");
            logger.Information("Done");

            loggerFactory.Dispose();
        }

        static LoggingContext GetCurrentLoggingContext()
        {
            if (LoggingContextIsSet()) return _currentLoggingContext.Value;

            var loggingContext = new LoggingContext
            {
                Application = Guid.Parse("919ce6c5-a9f6-4d6f-a2e2-1f9d717e7263"),
                BoundedContext = Guid.Parse("fd7d3776-0ed0-4ddd-85be-f1ad7455491e"),
                CorrelationId = CorrelationId.New(),
                Environment = Dolittle.Execution.Environment.Development,
                TenantId = TenantId.Development
            };

            _currentLoggingContext.Value = loggingContext;
            return loggingContext;
        }

        static bool LoggingContextIsSet() => _currentLoggingContext?.Value != null;
    }
}
