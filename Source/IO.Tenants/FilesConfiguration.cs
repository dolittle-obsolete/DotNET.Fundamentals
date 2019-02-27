/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Configuration;
using Dolittle.Lifecycle;

namespace Dolittle.IO.Tenants
{
    /// <summary>
    /// Represents the <see cref="IConfigurationObject"/> for <see cref="Files"/>
    /// </summary>
    [Singleton]
    public class FilesConfiguration
    {
        /// <summary>
        /// Gets the root path of the file system to be used for tenants
        /// </summary>
        public string RootPath { get; set; }

    }
}