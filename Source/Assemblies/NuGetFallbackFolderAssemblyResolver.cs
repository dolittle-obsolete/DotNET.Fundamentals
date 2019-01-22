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
    /// 
    /// </summary>
    /// <remarks>
    /// Linux / macOS : /usr/local/share/dotnet/sdk/NuGetFallbackFolder/{package path}
    /// Windows       : C:/Program Files/dotnet/sdk/NuGetFallbackFolder/{package path} 
    /// </remarks>
    public class NuGetFallbackFolderAssemblyResolver : ICompilationAssemblyResolver
    {
        /// <inheritdoc/>
        public bool TryResolveAssemblyPaths(CompilationLibrary library, List<string> assemblies)
        {
            var basePath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)?
                            @"c:\Program Files\dotnet\sdk\NuGetFallbackFolder":
                            "/usr/local/share/dotnet/sdk/NuGetFallbackFolder";

            if (!Directory.Exists(basePath)) return false;
            
            var found = false;

            var libraryBasePath = Path.Combine(basePath,library.Path);
            foreach( var assembly in library.Assemblies )
            {
                var assemblyPath = Path.Combine(libraryBasePath, assembly);
                if( File.Exists(assemblyPath))
                {
                    assemblies.Add(assemblyPath);
                    found = true;
                }
            }

            return found;
        }
    }

}