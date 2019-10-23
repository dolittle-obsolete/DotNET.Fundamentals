/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Represents a <see cref="ICompilationAssemblyResolver"/> that tries to resolve from the package runtime shared store
    /// <remarks>
    /// Read more here https://natemcmaster.com/blog/2018/08/29/netcore-primitives-2/
    /// https://github.com/dotnet/corefx/issues/11639
    /// </remarks>
    /// </summary>
    public class PackageRuntimeShareAssemblyResolver : ICompilationAssemblyResolver
    {
        /// <inheritdoc/>
        public bool TryResolveAssemblyPaths(CompilationLibrary library, List<string> assemblies)
        {
            string basePath;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                basePath = @"c:\Program Files\dotnet\shared";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // default location on Ubuntu 19.04
                basePath = "/usr/share/dotnet/shared";
            }
            else 
            {
                // keep the OSX location as the default
                basePath = "/usr/local/share/dotnet/shared";
            }

            var found = false;

            foreach (var path in Directory.GetDirectories(basePath))
            {
                if (found) break;
                var versionDir = Path.Combine(path, library.Version);
                if (Directory.Exists(versionDir))
                {
                    foreach (var file in Directory.GetFiles(versionDir))
                    {
                        if (found) break;
                        if (Path.GetFileName(file).ToLower().Equals(library.Name.ToLower() + ".dll"))
                        {
                            assemblies.Add(file);
                            found = true;
                        }
                    }
                }
            }

            return found;
        }
    }
}
