// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace Dolittle.Assemblies
{
    /// <summary>
    /// Represents an implementation of <see cref="ICompilationAssemblyResolver"/>.
    /// </summary>
    public class PackageCompilationAssemblyResolver : ICompilationAssemblyResolver
    {
        readonly string[] _nugetPackageDirectories;

        /// <summary>
        /// Initializes a new instance of <see cref="PackageCompilationAssemblyResolver"/>
        /// </summary>
        public PackageCompilationAssemblyResolver()
        {
            _nugetPackageDirectories = GetDefaultProbeDirectories();
        }

        /// <inheritdoc/>
        public bool TryResolveAssemblyPaths(CompilationLibrary library, List<string> assemblies)
        {
            if (_nugetPackageDirectories == null || _nugetPackageDirectories.Length == 0 ||
                !string.Equals(library.Type, "package", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            foreach (var directory in _nugetPackageDirectories)
            {
                if (TryResolvePackagePath(library, directory, out string packagePath))
                {
                    if (TryResolveFromPackagePath(library, packagePath, out IEnumerable<string> fullPathsFromPackage))
                    {
                        if (fullPathsFromPackage.Any())
                        {
                            assemblies.AddRange(fullPathsFromPackage);
                        }
                        else
                        {
                            var libPath = Path.Join(packagePath, "lib");
                            var dllName = $"{library.Name}.dll";
                            var paths = Directory.EnumerateFiles(libPath, dllName, SearchOption.AllDirectories);
                            assemblies.AddRange(paths);
                        }

                        return true;
                    }
                }
            }
            return false;
        }

        static string[] GetDefaultProbeDirectories()
        {
            var probeDirectories = AppDomain.CurrentDomain.GetData("PROBING_DIRECTORIES");
            var listOfDirectories = probeDirectories as string;

            if (!string.IsNullOrEmpty(listOfDirectories))
            {
                return listOfDirectories.Split(new char[] { Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries);
            }

            var packageDirectory = Environment.GetEnvironmentVariable("NUGET_PACKAGES");

            if (!string.IsNullOrEmpty(packageDirectory))
            {
                return new string[] { packageDirectory };
            }

            string basePath;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                basePath = Environment.GetEnvironmentVariable("USERPROFILE");
            }
            else
            {
                basePath = Environment.GetEnvironmentVariable("HOME");
            }

            if (string.IsNullOrEmpty(basePath))
            {
                return new string[] { string.Empty };
            }

            return new string[] { Path.Combine(basePath, ".nuget", "packages") };
        }

        static bool TryResolveFromPackagePath(CompilationLibrary library, string basePath, out IEnumerable<string> results)
        {
            var paths = new List<string>();

            foreach (var assembly in library.Assemblies)
            {
                if (!TryResolveAssemblyFile(basePath, assembly, out string fullName))
                {
                    // if one of the files can't be found, skip this package path completely.
                    // there are package paths that don't include all of the "ref" assemblies 
                    // (ex. ones created by 'dotnet store')
                    results = null;
                    return false;
                }

                paths.Add(fullName);
            }

            results = paths;
            return true;
        }

        static bool TryResolvePackagePath(CompilationLibrary library, string basePath, out string packagePath)
        {
            var path = library.Path;
            if (string.IsNullOrEmpty(path))
            {
                path = Path.Combine(library.Name, library.Version);
            }

            packagePath = Path.Combine(basePath, path);

            return Directory.Exists(packagePath);
        }

        static bool TryResolveAssemblyFile(string basePath, string assemblyPath, out string fullName)
        {
            fullName = Path.Combine(basePath, assemblyPath);
            return File.Exists(fullName);
        }
    }
}