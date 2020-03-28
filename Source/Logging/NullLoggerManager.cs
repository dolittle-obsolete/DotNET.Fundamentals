// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Lifecycle;

namespace Dolittle.Logging
{
    /// <summary>
    /// Represents an implementation of <see cref="ILoggerManager"/> that creates loggers that does nothing.
    /// </summary>
    [Singleton]
    internal class NullLoggerManager : ILoggerManager
    {
        private NullLoggerManager()
        {
        }

        /// <summary>
        /// Gets the static singleton instance of the <see cref="NullLoggerManager"/>.
        /// </summary>
        public static ILoggerManager Instance { get; } = new NullLoggerManager();

        /// <inheritdoc/>
        public void AddLogMessageWriterCreators(params ILogMessageWriterCreator[] creators)
        {
        }

        /// <inheritdoc/>
        public ILogger<T> CreateLogger<T>() => NullLogger<T>.TypedInstance;

        /// <inheritdoc/>
        public ILogger CreateLogger(Type type) => NullLogger.Instance;
    }
}
