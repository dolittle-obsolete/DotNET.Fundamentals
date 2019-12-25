// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;

namespace Dolittle.Logging.Json
{
    /// <summary>
    /// Represents a <see cref="ILogAppender"/> for appending JSON formatted log strings.
    /// </summary>
    public class JsonLogAppender : ILogAppender
    {
        readonly GetCurrentLoggingContext _getCurrentLoggingContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonLogAppender"/> class.
        /// </summary>
        /// <param name="getCurrentLoggingContext">A <see cref="GetCurrentLoggingContext"/> for getting current logging context.</param>
        public JsonLogAppender(GetCurrentLoggingContext getCurrentLoggingContext)
        {
            _getCurrentLoggingContext = getCurrentLoggingContext;
        }

        /// <inheritdoc/>
        public void Append(string filePath, int lineNumber, string member, LogLevel level, string message, Exception exception = null)
        {
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

        static string LogLevelAsString(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Critical:
                    return "fatal";
                case LogLevel.Error:
                    return "error";
                case LogLevel.Warning:
                    return "warn";
                case LogLevel.Info:
                    return "info";
                case LogLevel.Debug:
                    return "debug";
                case LogLevel.Trace:
                    return "trace";
            }

            return string.Empty;
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
