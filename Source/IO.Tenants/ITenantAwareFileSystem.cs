/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Execution;

namespace Dolittle.IO.Tenants
{
    /// <summary>
    /// Defines a file system that will provide a sandbox specific to the current tenant in the <see cref="ExecutionContext"/>
    /// </summary>
    public interface ITenantAwareFileSystem
    {
        /// <summary>
        /// Check if a file exists based on the relative path within the tenants sandbox
        /// </summary>
        /// <param name="relativePath">Relative path to check</param>
        /// <returns>True if exists, false if not</returns>
        bool Exists(string relativePath);

        /// <summary>
        /// Read all text from a relative path within the tenant
        /// </summary>
        /// <param name="relativePath">Relative path to the file to read from</param>
        /// <returns>Content of the file</returns>
        string ReadAllText(string relativePath);
    }
}