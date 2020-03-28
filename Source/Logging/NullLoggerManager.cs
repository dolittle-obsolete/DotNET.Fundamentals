// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Logging
{
    /// <summary>
    /// Represents an implementation of <see cref="ILoggerManager"/> that creates loggers that does nothing.
    /// </summary>
    internal class NullLoggerManager : ILoggerManager
    {
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
