/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Microsoft.Extensions.Logging;
using Dolittle.Logging;

namespace Dolittle.Booting.Stages
{
    /// <summary>
    /// Represents the settings for <see cref="BootStage.Logging"/> stage
    /// </summary>
    public class LoggingSettings : IRepresentSettingsForBootStage
    {
        /// <summary>
        /// Gets the <see cref="ILogAppender"/> to use
        /// </summary>
        public ILogAppender LogAppender { get; internal set; }


        /// <summary>
        /// Gets the <see cref="ILoggerFactory"/> to use
        /// </summary>
        public ILoggerFactory LoggerFactory { get; internal set; }

        /// <summary>
        /// Gets the <see cref="Dolittle.Logging.ILogger"/> to use - if this is null - it has not been set, use the default one
        /// </summary>
        public Dolittle.Logging.ILogger Logger { get; internal set; }
    }
}
