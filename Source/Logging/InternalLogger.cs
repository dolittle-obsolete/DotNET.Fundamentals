// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Logging
{
    /// <summary>
    /// Represents an implementation of <see cref="ILogger"/>.
    /// </summary>
    internal abstract class InternalLogger : ILogger
    {
        public ILogMessageWriter[] LogMessageWriters { get; set; }

        /// <inheritdoc/>
        public void Trace(string message, params object[] args) => Write(LogLevel.Trace, message, args);

        /// <inheritdoc/>
        public void Trace(Exception exception, string message, params object[] args) => Write(LogLevel.Trace, exception, message, args);

        /// <inheritdoc/>
        public void Debug(string message, params object[] args) => Write(LogLevel.Debug, message, args);

        /// <inheritdoc/>
        public void Debug(Exception exception, string message, params object[] args) => Write(LogLevel.Debug, exception, message, args);

        /// <inheritdoc/>
        public void Information(string message, params object[] args) => Write(LogLevel.Info, message, args);

        /// <inheritdoc/>
        public void Information(Exception exception, string message, params object[] args) => Write(LogLevel.Info, exception, message, args);

        /// <inheritdoc/>
        public void Warning(string message, params object[] args) => Write(LogLevel.Warning, message, args);

        /// <inheritdoc/>
        public void Warning(Exception exception, string message, params object[] args) => Write(LogLevel.Warning, exception, message, args);

        /// <inheritdoc/>
        public void Critical(string message, params object[] args) => Write(LogLevel.Critical, message, args);

        /// <inheritdoc/>
        public void Critical(Exception exception, string message, params object[] args) => Write(LogLevel.Critical, exception, message, args);

        /// <inheritdoc/>
        public void Error(string message, params object[] args) => Write(LogLevel.Error, message, args);

        /// <inheritdoc/>
        public void Error(Exception exception, string message, params object[] args) => Write(LogLevel.Error, exception, message, args);

        void Write(LogLevel logLevel, string message, params object[] args)
        {
            var writers = LogMessageWriters;
            if (writers == null) return;

            for (var i = 0; i < writers.Length; ++i)
            {
                try
                {
                    writers[i].Write(logLevel, message, args);
                }
                catch
                {
                }
            }
        }

        void Write(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            var writers = LogMessageWriters;
            if (writers == null) return;

            for (var i = 0; i < writers.Length; ++i)
            {
                try
                {
                    writers[i].Write(logLevel, exception, message, args);
                }
                catch
                {
                }
            }
        }
    }
}
