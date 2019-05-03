/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Runtime.Loader;
using Dolittle.Assemblies;
using Dolittle.Booting;
using Dolittle.Collections;
using Dolittle.Strings;
using Dolittle.Types;
using Microsoft.Extensions.Logging.Abstractions;

namespace Dolittle.Build.CLI
{

    class Program
    {
        internal static BuildTarget BuildTarget;

        static int Main(string[] args)
        {
            try
            {
                var startTime = DateTime.UtcNow;

                var assemblyFile = args[0];
                var pluginAssemblies = args[1].Split(";");
                var configurationFile = args[2];

                if (string.IsNullOrEmpty(args[1]) ||
                    pluginAssemblies.Length == 0 ||
                    string.IsNullOrEmpty(configurationFile)) return 0;

                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyFile);
                var assemblyContext = AssemblyContext.From(assemblyFile);
                BuildTarget = new BuildTarget(assemblyFile, assembly, assemblyContext);

                Console.WriteLine("Performing Dolittle post-build steps");

                Console.WriteLine($"  Performing for: {assemblyFile}");
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
                var buildTaskPerformers = bootLoaderResult.Container.Get<IBuildTaskPerformers>();
                buildTaskPerformers.Perform();

                var assemblyModifiers = bootLoaderResult.Container.Get<ITargetAssemblyModifiers>();
                assemblyModifiers.ModifyAndSave();

                var postTasksPerformers = bootLoaderResult.Container.Get<IPostBuildTaskPerformers>();
                postTasksPerformers.Perform();

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