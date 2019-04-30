/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Build
{
    /// <summary>
    /// Represents the configuration for the build
    /// </summary>
    public class BuildConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BuildConfiguration"/>
        /// </summary>
        /// <param name="targetAssemblyPath">Path of the target assembly being built</param>
        public BuildConfiguration(string targetAssemblyPath)
        {
            TargetAssemblyPath = targetAssemblyPath;
        }

        /// <summary>
        /// Gets the path of the target assembly being build
        /// </summary>
        public string TargetAssemblyPath { get; }
    }
}