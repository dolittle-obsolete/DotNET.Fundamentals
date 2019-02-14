/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using System.Text.RegularExpressions;
using Dolittle.Execution;

namespace Dolittle.IO.Tenants
{  
    /// <summary>
    /// Represents an implementation of <see cref="ITenantAwareFileSystem"/>
    /// </summary>
    public class TenantAwareFileSystem : ITenantAwareFileSystem
    {
        static Regex _windowsPathRooted = new Regex("^[a-z]*:");

        readonly IExecutionContextManager _executionContextManager;
        readonly IFileSystem _fileSystem;
        private readonly TenantAwareFileSystemConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of <see cref="TenantAwareFileSystem"/>
        /// </summary>
        /// <param name="executionContextManager"><see cref="IExecutionContextManager"/> to use for determining the current tenant</param>
        /// <param name="configuration"><see cref="TenantAwareFileSystemConfiguration">Configuration</see></param>
        /// <param name="fileSystem">Underlying <see cref="IFileSystem"/></param>
        public TenantAwareFileSystem(
            IExecutionContextManager executionContextManager,
            TenantAwareFileSystemConfiguration configuration,
            IFileSystem fileSystem)
        {
            _executionContextManager = executionContextManager;
            _fileSystem = fileSystem;
            _configuration = configuration;
        }

        /// <inheritdoc/>
        public bool Exists(string relativePath)
        {
            ThrowIfAccessingOutsideTenantSandbox(relativePath);
            var absolutePath = MapPath(relativePath);
            return _fileSystem.Exists(absolutePath);
        }

        /// <inheritdoc/>
        public string ReadAllText(string relativePath)
        {
            ThrowIfAccessingOutsideTenantSandbox(relativePath);
            var absolutePath = MapPath(relativePath);
            return _fileSystem.ReadAllText(absolutePath);
        }

        string MapPath(string relativePath)
        {
            return Path.Combine(
                _configuration.RootPath,
                _executionContextManager.Current.Tenant.ToString(),
                relativePath);
        }

        void ThrowIfAccessingOutsideTenantSandbox(string path)
        {
            // Note: Path.IsPathRooted would've been perfect for this. But it is platform specific at runtime and 
            // don't feel comfortable having specs that are platform specific for this - so hand-rolling the support because of that
            
            var outside = 
                path.StartsWith("..") ||
                path.StartsWith("\\") ||
                path.StartsWith("/") ||
                _windowsPathRooted.IsMatch(path);

            if( outside ) throw new AccessOutsideSandboxDenied(path);
        }
    }
}