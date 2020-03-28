// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Lifecycle;

namespace Dolittle.Logging
{
    /// <summary>
    /// An implementation of <see cref="ILoggerManager"/>.
    /// </summary>
    [Singleton]
    internal class LoggerManager : ILoggerManager
    {
        readonly IDictionary<Type, Logger> _loggers;
        ILogMessageWriterCreator[] _creators;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerManager"/> class.
        /// </summary>
        public LoggerManager()
        {
            _loggers = new Dictionary<Type, Logger>();
            _creators = new ILogMessageWriterCreator[] { null };
        }

        /// <inheritdoc/>
        public void AddLogMessageWriterCreators(params ILogMessageWriterCreator[] creators)
        {
            lock (_loggers)
            {
                _creators = creators;

                foreach ((var type, var logger) in _loggers)
                {
                    logger.LogMessageWriters = CreateWriters(type);
                }
            }
        }

        /// <inheritdoc/>
        public ILogger<T> CreateLogger<T>() => CreateLogger(typeof(T)) as ILogger<T>;

        /// <inheritdoc/>
        public ILogger CreateLogger(Type type)
        {
            lock (_loggers)
            {
                if (!_loggers.TryGetValue(type, out var logger))
                {
                    logger = Activator.CreateInstance(typeof(Logger<>).MakeGenericType(type)) as Logger;
                    logger.LogMessageWriters = CreateWriters(type);
                    _loggers[type] = logger;
                }

                return logger;
            }
        }

        ILogMessageWriter[] CreateWriters(Type type)
        {
            var writers = new ILogMessageWriter[_creators.Length];

            for (var i = 0; i < _creators.Length; ++i)
            {
                writers[i] = _creators[i].CreateFor(type);
            }

            return writers;
        }
    }
}
