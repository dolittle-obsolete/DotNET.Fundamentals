/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Dolittle.IO;

namespace Dolittle.Configuration.Files
{

    /// <summary>
    /// Represents an implementation of <see cref="ICanProvideConfigurationObjects"/> for files on disk
    /// </summary>
    /// <remarks>
    /// This provider looks for configuration files in a specific <see cref="FileConfigurationObjectsProvider.BaseFolder">folder</see>
    /// relative to current running directory. It also assumes a specific convention for filenames
    /// with the name of the <see cref="IConfigurationObject"/>. The name will be the same as provided
    /// in the <see cref="NameAttribute"/>, or fall back to the type name.
    /// </remarks>
    public class FileConfigurationObjectsProvider : ICanProvideConfigurationObjects
    {
        /// <summary>
        /// The base folder for Dolittle configuration files
        /// </summary>
        public const string BaseFolder = ".dolittle";
        
        readonly IFileSystem _fileSystem;
        readonly IConfigurationFileParsers _parsers;

        /// <summary>
        /// Initializes a new instance of <see cref="FileConfigurationObjectsProvider"/>
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <param name="parsers"></param>
        public FileConfigurationObjectsProvider(IFileSystem fileSystem, IConfigurationFileParsers parsers)
        {
            _fileSystem = fileSystem;
            _parsers = parsers;
        }

        /// <inheritdoc/>
        public bool CanProvide(Type type)
        {
            var filename = GetFilenameFor(type);
            return _fileSystem.Exists(filename);
        }

        /// <inheritdoc/>
        public object Provide(Type type)
        {
            var filename = GetFilenameFor(type);
            var content = _fileSystem.ReadAllText(filename);
            var instance = _parsers.Parse(type, filename, content);
            return instance;           
        }

        string GetFilenameFor(Type type)
        {
            var path = Path.Combine(_fileSystem.GetCurrentDirectory(), BaseFolder, type.GetFriendlyConfigurationName(), ".json");
            return path;
        }
    }
}