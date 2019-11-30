// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Logging
{
    /// <summary>
    /// Defines an appender to the log.
    /// </summary>
    public interface ILogAppender
    {
        /// <summary>
        /// Append message to the log.
        /// </summary>
        /// <param name="filePath">FilePath of origin of the message.</param>
        /// <param name="lineNumber">LineNumber within the source file.</param>
        /// <param name="member">Member of the type of the origin of the message.</param>
        /// <param name="level"><see cref="LogLevel">Severity</see> of the entry.</param>
        /// <param name="message">Message to log.</param>
        /// <param name="exception">Optional exception.</param>
        void Append(string filePath, int lineNumber, string member, LogLevel level, string message, Exception exception = null);
    }
}
