// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Dolittle.Logging.Json
{
    /// <summary>
    /// Represents a <see cref="ILogAppender"/> for appending JSON formatted log strings.
    /// </summary>
    public class JsonLogAppender : ILogAppender
    {
        readonly GetCurrentLoggingContext _getCurrentLoggingContext;
        readonly ILoggerFactory _loggerFactory;
        readonly Dictionary<string, Microsoft.Extensions.Logging.ILogger> _loggers = new Dictionary<string, Microsoft.Extensions.Logging.ILogger>();

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonLogAppender"/> class.
        /// </summary>
        /// <param name="getCurrentLoggingContext">A <see cref="GetCurrentLoggingContext"/> for getting current logging context.</param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> to use.</param>
        public JsonLogAppender(GetCurrentLoggingContext getCurrentLoggingContext, ILoggerFactory loggerFactory)
        {
            _getCurrentLoggingContext = getCurrentLoggingContext;
            _loggerFactory = loggerFactory;
        }

        /// <inheritdoc/>
        public void Append(string filePath, int lineNumber, string member, LogLevel level, string message, Exception exception = null)
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

            if (!logger.IsEnabled(Translate(level))) return;

            var writer = ChooseWriter(level);
            var logMessage = CreateLogMessage(filePath, lineNumber, member, message, LogLevelAsString(level), exception);

            var jsonMessage = logMessage.ToJson();
            writer.WriteLine(jsonMessage);
        }

        static TextWriter ChooseWriter(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Critical:
                case LogLevel.Error:
                    return Console.Error;
            }

            return Console.Out;
        }

        Microsoft.Extensions.Logging.LogLevel Translate(LogLevel level) => level switch
        {
            LogLevel.Trace => Microsoft.Extensions.Logging.LogLevel.Trace,
            LogLevel.Debug => Microsoft.Extensions.Logging.LogLevel.Debug,
            LogLevel.Info => Microsoft.Extensions.Logging.LogLevel.Information,
            LogLevel.Warning => Microsoft.Extensions.Logging.LogLevel.Warning,
            LogLevel.Critical => Microsoft.Extensions.Logging.LogLevel.Critical,
            LogLevel.Error => Microsoft.Extensions.Logging.LogLevel.Error,
            _ => Microsoft.Extensions.Logging.LogLevel.None
        };

        string LogLevelAsString(LogLevel level)
        {
            return level switch
            {
                LogLevel.Critical => "fatal",
                LogLevel.Error => "error",
                LogLevel.Warning => "warn",
                LogLevel.Info => "info",
                LogLevel.Debug => "debug",
                LogLevel.Trace => "trace",
                _ => string.Empty,
            };
        }

        JsonLogMessage CreateLogMessage(string filePath, int lineNumber, string member, string message, string logLevel, Exception exception = null)
        {
            return new JsonLogMessage(
                logLevel,
                DateTimeOffset.Now,
                _getCurrentLoggingContext(),
                filePath,
                lineNumber,
                member,
                message,
                exception?.StackTrace ?? string.Empty);
        }
    }
}
