// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Logging;
using Dolittle.Logging.Json;
using Microsoft.Extensions.Logging;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the settings for <see cref="BootStage.Logging"/> stage
    /// If the UseDefault flag is set, the <see cref="DefaultLogAppender" /> will be used regardless of environment.
    /// If the UseDefault flag is not set, the <see cref="JsonLogAppender" /> will be used in the production environment,
    /// while the <see cref="DefaultLogAppender" /> will be used in all other cases.
    /// </summary>
    public class LoggingSettings : IRepresentSettingsForBootStage
    {
        /// <summary>
        /// Gets the <see cref="ILogAppender"/> to use.
        /// </summary>
        public ILogAppender LogAppender { get; internal set; }

        /// <summary>
        /// Gets the <see cref="ILoggerFactory"/> to use.
        /// </summary>
        public ILoggerFactory LoggerFactory { get; internal set; }

        /// <summary>
        /// Gets the <see cref="Dolittle.Logging.ILogger"/> to use - if this is null - it has not been set,
        /// use the logger determined by the default setting and environment.
        /// </summary>
        public Dolittle.Logging.ILogger Logger { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the default logger should be used or not in all environments.
        /// </summary>
        public bool UseDefaultInAllEnvironments { get; internal set; }
    }
}
