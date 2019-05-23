/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Dolittle.Build.MSBuild.Tasks
{
    /// <summary>
    /// Represents a task that generates a temporary file in the %tmp% folder
    /// </summary>
    public class TempFileGenerator : Task
    {
        /// <summary>
        /// Gets or sets the filename generated
        /// </summary>
        [Output]
        public string FileName { get; set; }

        /// <inheritdoc/>
        public override bool Execute()
        {
            FileName = Path.GetTempFileName();
            return true;
        }
    }
}