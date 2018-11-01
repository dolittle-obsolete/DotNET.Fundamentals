/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Dolittle.Logging
{

    /// <summary>
    /// Represents a default implementation of <see cref="ILogAppender"/> for using ILogger
    /// </summary>
    public class DefaultLogAppender : ILogAppender
    {
        GetCurrentLoggingContext _getCurrentLoggingContext;
        ILoggerFactory _loggerFactory;
        Dictionary<string, Microsoft.Extensions.Logging.ILogger> _loggers = new Dictionary<string, Microsoft.Extensions.Logging.ILogger>();

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultLogAppender"/>
        /// </summary>
        /// <param name="getCurrentLoggingContext"></param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> to use</param>
        public DefaultLogAppender(GetCurrentLoggingContext getCurrentLoggingContext, ILoggerFactory loggerFactory)
        {
            _getCurrentLoggingContext = getCurrentLoggingContext;
            _loggerFactory = loggerFactory;
        }

        /// <inheritdoc/>
        public void Append(string filePath, int lineNumber, string member, LogLevel level, string message, Exception exception = null)
        {
            Microsoft.Extensions.Logging.ILogger logger;
            var loggingContext = _getCurrentLoggingContext();
            
            var loggerKey = Path.GetFileNameWithoutExtension(filePath);
            if (!_loggers.ContainsKey(loggerKey))
            {
                logger = _loggerFactory.CreateLogger(loggerKey);
                _loggers[loggerKey] = logger;
            }
            else logger = _loggers[loggerKey];

            message = $"[{member}({lineNumber})]-{message}";

            switch( level )
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
