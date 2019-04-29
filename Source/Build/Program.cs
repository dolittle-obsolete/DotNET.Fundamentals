/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Dolittle.Assemblies;
using Dolittle.Booting;
using Dolittle.Collections;
using Dolittle.Types;
using Microsoft.Extensions.Logging.Abstractions;

namespace Dolittle.Build
{

    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Performing Dolittle post-build steps");

            var assembly = args[0];
            var pluginAssemblies = args[1].Split(";");

            Console.WriteLine($"  Performing for: {assembly}");
            Console.WriteLine("  Using plugins from: ");

            foreach (var pluginAssembly in pluginAssemblies)
                Console.WriteLine($"    {pluginAssembly}");

            var bootLoaderResult = Bootloader.Configure(_ => _
                .WithAssemblyProvider(new AssemblyProvider(new Dolittle.Logging.NullLogger(),pluginAssemblies))
                .NoLogging()
                .SkipBootprocedures()
            ).Start();
            var buildMessages = bootLoaderResult.Container.Get<IBuildMessages>();

            var performers = bootLoaderResult.Container.Get<IPostBuildTaskPerformers>();
            performers.Perform();

            return 0;
        }
    }
}