/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.DependencyInversion;
using Dolittle.Types;

namespace Dolittle.Configuration
{
    /// <summary>
    /// Represents an implementation of <see creF="IConfigurationFileParsers"/>
    /// </summary>
    public class ConfigurationFileParsers : IConfigurationFileParsers
    {
        readonly ITypeFinder _typeFinder;

        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationFileParsers"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> to use for finding parsers</param>
        /// <param name="container"><see cerf="IContainer"/> used to get instances</param>
        public ConfigurationFileParsers(ITypeFinder typeFinder, IContainer container)
        {
            _typeFinder = typeFinder;
        }

        /// <inheritdoc/>
        public object Parse(Type type, string content)
        {
            throw new NotImplementedException();
        }
    }
}