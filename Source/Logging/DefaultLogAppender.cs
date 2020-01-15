// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Dolittle.Logging
{
    /// <summary>
    /// Represents a default implementation of <see cref="ILogAppender"/> for using ILogger.
    /// The default logger is derived from Microsoft.Extensions.Logging and can be filtered / configured as with the standard aspnetcore logger.
    /// </summary>
    public class DefaultLogAppender : ILogAppender
    {
        readonly GetCurrentLoggingContext _getCurrentLoggingContext;
        readonly ILoggerFactory _loggerFactory;
        readonly Dictionary<string, Microsoft.Extensions.Logging.ILogger> _loggers = new Dictionary<string, Microsoft.Extensions.Logging.ILogger>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultLogAppender"/> class.
        /// </summary>
        /// <param name="getCurrentLoggingContext">A <see cref="GetCurrentLoggingContext"/> for getting current logging context.</param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> to use.</param>
        public DefaultLogAppender(GetCurrentLoggingContext getCurrentLoggingContext, ILoggerFactory loggerFactory)
        {
            _getCurrentLoggingContext = getCurrentLoggingContext;
            _loggerFactory = loggerFactory;
        }

        /// <inheritdoc/>
        public void Append(
            string filePath,
            int lineNumber,
            string member,
            LogLevel level,
            string message,
            Exception exception = null)
        {
            Microsoft.Extensions.Logging.ILogger logger;

            var loggerKey = Path.GetFileNameWithoutExtension(filePath);
            if (!_loggers.ContainsKey(loggerKey))
            {
                logger = _loggerFactory.CreateLogger(loggerKey);
                _loggers[loggerKey] = logger;
            }
            else
            {
                logger = _loggers[loggerKey];
            }

            message = $"[{member}({lineNumber})]-{message}";

            switch (level)
            {
                case LogLevel.Trace: logger.LogTrace(message); break;
                case LogLevel.Debug: logger.LogDebug(message); break;
                case LogLevel.Info: logger.LogInformation(message); break;
                case LogLevel.Warning: logger.LogWarning(message); break;
                case LogLevel.Critical: logger.LogCritical(message); break;
                case LogLevel.Error: logger.LogError(0, exception, message); break;
            }
        }
    }
}
