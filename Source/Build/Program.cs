/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;

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
            Console.WriteLine( "  Using plugins from: ");

            foreach( var pluginAssembly in pluginAssemblies )
                Console.WriteLine($"    {pluginAssembly}");

            //

            return 0;
        }
    }
}