﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Dolittle.Logging
{
    /// <summary>
    /// Defines the configuration for logging - see <see cref="ILogger"/>.
    /// </summary>
    public interface ILogAppenders
    {
        /// <summary>
        /// Gets the configured <see cref="ILogAppender">appenders</see>.
        /// </summary>
        IEnumerable<ILogAppender> Appenders { get; }

        /// <summary>
        /// Add an appender for logging.
        /// </summary>
        /// <param name="appender"><see cref="ILogAppender"/> to add.</param>
        void Add(ILogAppender appender);

        /// <summary>
        /// Remove all the log appenders.
        /// </summary>
        void Clear();

        /// <summary>
        /// Append message to all the <see cref="ILogAppender">log appenders</see>.
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
