// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Lifecycle;

namespace Dolittle.Logging
{
    /// <summary>
    /// Represents an implementation of <see cref="ILogAppenders"/>.
    /// </summary>
    [Singleton]
    public class LogAppenders : ILogAppenders
    {
        readonly List<ILogAppender> _appenders = new List<ILogAppender>();

        /// <summary>
        /// Initializes a new instance of the <see cref="LogAppenders"/> class.
        /// </summary>
        /// <param name="logAppendersConfigurators"><see cref="IEnumerable{T}">Instances of <see cref="ICanConfigureLogAppenders"/></see>.</param>
        /// <param name="defaultLogAppender">Default <see cref="ILogAppender"/> - if any - optional.</param>
        public LogAppenders(
            IEnumerable<ICanConfigureLogAppenders> logAppendersConfigurators,
            ILogAppender defaultLogAppender = null)
        {
            if (defaultLogAppender != null) Add(defaultLogAppender);
            logAppendersConfigurators.ForEach(l => l.Configure(this));
        }

        /// <inheritdoc/>
        public IEnumerable<ILogAppender> Appenders => _appenders;

        /// <inheritdoc/>
        public void Add(ILogAppender appender)
        {
            _appenders.Add(appender);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _appenders.Clear();
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
            _appenders.ForEach(l =>
            {
                try
                {
                    l.Append(filePath, lineNumber, member, level, message, exception);
                }
                catch
                {
                    // Ignore any errors from any of the appenders - we don't care
                }
            });
        }
    }
}
