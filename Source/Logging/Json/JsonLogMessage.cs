// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Logging.Json
{
    /// <summary>
    /// Defines the log message for the <see cref="JsonLogAppender"/>.
    /// </summary>
    public class JsonLogMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonLogMessage"/> class.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="timestamp">Log timestamp.</param>
        /// <param name="loggingContext">The <see cref="LoggingContext"/>.</param>
        /// <param name="filePath">The path to the file logging.</param>
        /// <param name="lineNumber">The line number into the file logging.</param>
        /// <param name="member">The member causing logging.</param>
        /// <param name="message">Message to log.</param>
        /// <param name="stackTrace">Stacktrace, if there is one.</param>
        public JsonLogMessage(
            string logLevel,
            DateTimeOffset timestamp,
            LoggingContext loggingContext,
            string filePath,
            int lineNumber,
            string member,
            string message,
            string stackTrace)
        {
            LogLevel = logLevel;
            Timestamp = timestamp;
            Application = loggingContext.Application;
            BoundedContext = loggingContext.BoundedContext;
            TenantId = loggingContext.TenantId;
            Environment = loggingContext.Environment;
            CorrelationId = loggingContext.CorrelationId;
            FilePath = filePath;
            LineNumber = lineNumber;
            Member = member;
            Message = message;
            StackTrace = stackTrace;
        }

        /// <summary>
        /// Gets the level of severity of the logging.
        /// </summary>
        public string LogLevel { get; }

        /// <summary>
        /// Gets the timestamp of when the logging occurred.
        /// </summary>
        public DateTimeOffset Timestamp { get; }

        /// <summary>
        /// Gets the Application Id.
        /// </summary>
        public Guid Application { get; }

        /// <summary>
        /// Gets the BoundedContext Id.
        /// </summary>
        public Guid BoundedContext { get; }

        /// <summary>
        /// Gets the Tenant Id.
        /// </summary>
        public Guid TenantId { get; }

        /// <summary>
        /// Gets the environment of the process that logged this message.
        /// </summary>
        public string Environment { get; }

        /// <summary>
        /// Gets the Correlation Id.
        /// </summary>
        public Guid CorrelationId { get; }

        /// <summary>
        /// Gets the Filepath source of the log message.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Gets the line number of the source of the log message.
        /// </summary>
        public int LineNumber { get; }

        /// <summary>
        /// Gets the member of the source of the log message.
        /// </summary>
        public string Member { get; }

        /// <summary>
        /// Gets the log message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the exception's stacktrace.
        /// </summary>
        public string StackTrace { get; }
    }
}