// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Lifecycle;

namespace Dolittle.Logging
{
    /// <summary>
    /// Represents an implementation of <see cref="ILogger"/>.
    /// </summary>
    [Singleton]
    public class Logger : ILogger
    {
        readonly ILogAppenders _logAppenders;

        static Logger() => Internal = new NullLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="logAppenders">The <see cref="ILogAppenders">log appenders</see>.</param>
        public Logger(ILogAppenders logAppenders)
        {
            _logAppenders = logAppenders;
            Internal = this;
        }

        /// <summary>
        /// Gets the internal logger for those scenarios where it can't be or it is inconvenient to get it injected.
        /// </summary>
        public static ILogger Internal { get; private set; }

        /// <inheritdoc/>
        public void Trace(string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Trace, message);
        }

        /// <inheritdoc/>
        public void Debug(string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Debug, message);
        }

        /// <inheritdoc/>
        public void Information(string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Info, message);
        }

        /// <inheritdoc/>
        public void Warning(string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Warning, message);
        }

        /// <inheritdoc/>
        public void Critical(string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Critical, message);
        }

        /// <inheritdoc/>
        public void Error(string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Error, message);
        }

        /// <inheritdoc/>
        public void Error(Exception exception, string message, string filePath, int lineNumber, string member)
        {
            _logAppenders.Append(filePath, lineNumber, member, LogLevel.Error, message, exception);
        }
    }
}
