/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Configuration;
using Dolittle.Lifecycle;

namespace Dolittle.IO.Tenants
{
    /// <summary>
    /// Represents the <see cref="IConfigurationObject"/> for <see cref="TenantAwareFileSystem"/>
    /// </summary>
    [Name("tenant-files")]
    [Singleton]
    public class TenantAwareFileSystemConfiguration : IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TenantAwareFileSystemConfiguration"/>
        /// </summary>
        /// <param name="rootPath">The root path for the file system where tenants live</param>
        public TenantAwareFileSystemConfiguration(string rootPath)
        {
            RootPath = rootPath;
        }

        /// <summary>
        /// Gets the root path of the file system to be used for tenants
        /// </summary>
        public string RootPath { get; }

    }
}