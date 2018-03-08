/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.IO;

namespace Dolittle.IO
{
    /// <summary>
    /// Represents an implementation of <see cref="IFileSystem"/>
    /// </summary>
    public class FileSystem : IFileSystem
    {
        /// <inheritdoc/>
        public IEnumerable<FileInfo> GetFilesFrom(string path, string searchPattern)
        {
            return new DirectoryInfo(path).GetFiles(searchPattern);
        }
    }
}
