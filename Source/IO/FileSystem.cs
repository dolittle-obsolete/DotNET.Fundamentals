/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
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
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <inheritdoc/>
        public string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        /// <inheritdoc/>
        public IEnumerable<FileInfo> GetFilesFrom(string path, string searchPattern)
        {
            return new DirectoryInfo(path).GetFiles(searchPattern);
        }

        /// <inheritdoc/>
        public string ReadAllText(string filename)
        {
            return File.ReadAllText(filename);
        }
    }
}
