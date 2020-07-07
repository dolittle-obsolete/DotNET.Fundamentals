// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Threading;
using Dolittle.Execution;
using Dolittle.Logging;
using Dolittle.Scheduling;
using Dolittle.Tenancy;
using Microsoft.Extensions.Logging;

namespace TypeSystem
{
    static class Program
    {
        static readonly AsyncLocal<LoggingContext> _currentLoggingContext = new AsyncLocal<LoggingContext>();

        static void Main()
        {
            var loggerFactory = LoggerFactory.Create(_ => _.AddConsole());
            var logAppender = new DefaultLogAppender(GetCurrentLoggingContext, loggerFactory);
            var logAppenders = new LogAppenders(Array.Empty<ICanConfigureLogAppenders>(), logAppender);
            var logger = new Logger(logAppenders) as Dolittle.Logging.ILogger;

            logger.Information("Starting assembly systems boot");

            var assemblies = Dolittle.Assemblies.Bootstrap.Boot.Start(logger);
            var typeFinder = Dolittle.Types.Bootstrap.Boot.Start(assemblies, new AsyncScheduler(), logger);

            logger.Information($"Indexed '{typeFinder.All.Count()}' types");

            foreach (var controller in typeFinder.FindMultiple(typeof(ISomething)))
            {
                logger.Information($"Found '{controller.FullName}' implementation");
            }

            logger.Information("Done");

            loggerFactory.Dispose();
        }

        static LoggingContext GetCurrentLoggingContext()
        {
            if (LoggingContextIsSet()) return _currentLoggingContext.Value;

            var loggingContext = new LoggingContext
            {
                Application = Guid.Parse("919ce6c5-a9f6-4d6f-a2e2-1f9d717e7263"),
                Microservice = Guid.Parse("fd7d3776-0ed0-4ddd-85be-f1ad7455491e"),
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
