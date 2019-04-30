/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Dolittle.Assemblies;
using Dolittle.Booting;
using Dolittle.Collections;
using Dolittle.Strings;
using Dolittle.Types;
using Microsoft.Extensions.Logging.Abstractions;

namespace Dolittle.Build
{

    class Program
    {
        internal static BuildConfiguration BuildConfiguration;

        static int Main(string[] args)
        {
            try
            {
                var startTime = DateTime.UtcNow;

                var assembly = args[0];
                var pluginAssemblies = args[1].Split(";");
                var configurationFile = args[2];

                if (string.IsNullOrEmpty(args[1]) ||
                    pluginAssemblies.Length == 0 ||
                    string.IsNullOrEmpty(configurationFile)) return 0;

                BuildConfiguration = new BuildConfiguration(assembly);

                Console.WriteLine("Performing Dolittle post-build steps");

                Console.WriteLine($"  Performing for: {assembly}");
                Console.WriteLine("  Using plugins from: ");

                foreach (var pluginAssembly in pluginAssemblies)
                    Console.WriteLine($"    {pluginAssembly}");

                var bootLoaderResult = Bootloader.Configure(_ => _
                    .WithAssemblyProvider(new AssemblyProvider(new Dolittle.Logging.NullLogger(), pluginAssemblies))
                    .NoLogging()
                    .SkipBootprocedures()
                ).Start();

                var buildMessages = bootLoaderResult.Container.Get<IBuildMessages>();

                var configuration = bootLoaderResult.Container.Get<IPerformerConfigurationManager>();
                configuration.Initialize(configurationFile);
                var performers = bootLoaderResult.Container.Get<IPostBuildTaskPerformers>();
                performers.Perform();

                var endTime = DateTime.UtcNow;
                var deltaTime = endTime.Subtract(startTime);
                buildMessages.Information($"Time Elapsed {deltaTime.ToString("G")} (Dolittle)");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error executing Dolittle post build tool".Red());
                Console.Error.WriteLine($"Exception: {ex.Message}".Red());
                Console.Error.WriteLine($"StackTrace: {ex.StackTrace}".Red());
                return 1;
            }

            return 0;
        }
    }
}