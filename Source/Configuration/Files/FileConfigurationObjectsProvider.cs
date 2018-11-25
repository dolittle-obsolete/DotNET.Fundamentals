/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using Dolittle.Collections;
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

        readonly string[] SearchPaths = new[] {
            BaseFolder,
            "..",
            "."
        };

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
            var foundPaths = new List<string>();
            SearchPaths.ForEach(_ => 
            {
                var filename = GetFilenameFor(type, _);
                if( _fileSystem.Exists(filename) ) foundPaths.Add(filename);
            });
            if( foundPaths.Count > 1 ) throw new MultipleFilesAvailableOfSameType(type, foundPaths);
            return foundPaths.Count > 0;
        }

        /// <inheritdoc/>
        public object Provide(Type type)
        {
            object instance = null;
            SearchPaths.ForEach(_ => 
            {
                var filename = GetFilenameFor(type, _);
                if( _fileSystem.Exists(filename) )
                {
                    var content = _fileSystem.ReadAllText(filename);
                    instance = _parsers.Parse(type, filename, content);
                } 
            });

            if( instance != null  ) return instance;
            throw new UnableToProvideConfigurationObject(typeof(FileConfigurationObjectsProvider), type);
        }

        string GetFilenameFor(Type type, string basePath)
        {
            var path = Path.Combine(_fileSystem.GetCurrentDirectory(), basePath, $"{type.GetFriendlyConfigurationName()}.json");
            return path;
        }
    }
}