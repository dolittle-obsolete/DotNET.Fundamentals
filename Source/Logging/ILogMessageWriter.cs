// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Logging
{
    /// <summary>
    /// Defines an system that writes log messages to a log.
    /// </summary>
    public interface ILogMessageWriter
    {
        /// <summary>
        /// Formats and writes a log message.
        /// </summary>
        /// <param name="logLevel">The <see cref="LogLevel"/> of the message.</param>
        /// <param name="message">Format string of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        void Write(LogLevel logLevel, string message, params object[] args);

        /// <summary>
        /// Formats and writes a log message.
        /// </summary>
        /// <param name="logLevel">The <see cref="LogLevel"/> of the message.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        void Write(LogLevel logLevel, Exception exception, string message, params object[] args);
    }
}
