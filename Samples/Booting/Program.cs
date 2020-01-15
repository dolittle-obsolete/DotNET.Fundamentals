// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Booting;
using Dolittle.Collections;
using Microsoft.Extensions.Logging;
using ILogger = Dolittle.Logging.ILogger;

namespace Booting
{
    static class Program
    {
        static void Main()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();

            var bootloader = Bootloader.Configure(_ => _
                .UseLoggerFactory(loggerFactory)
                .Development()
                .IncludeAssembliesStartingWith("Microsoft"));

            var result = bootloader.Start();
            var logger = result.Container.Get<ILogger>();
            result.Assemblies.GetAll().ForEach(_ => logger.Information($"Assembly '{_}' loaded and part of discovery"));

            loggerFactory.Dispose();
        }
    }
}
