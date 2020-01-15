// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Logging
{
    /// <summary>
    /// Represents a null implementation of <see cref="ILogger"/> - it does not do anything.
    /// </summary>
    public class NullLogger : ILogger
    {
        /// <inheritdoc/>
        public void Trace(string message, string filePath, int lineNumber, string member)
        {
        }

        /// <inheritdoc/>
        public void Debug(string message, string filePath, int lineNumber, string member)
        {
        }

        /// <inheritdoc/>
        public void Information(string message, string filePath, int lineNumber, string member)
        {
        }

        /// <inheritdoc/>
        public void Warning(string message, string filePath, int lineNumber, string member)
        {
        }

        /// <inheritdoc/>
        public void Critical(string message, string filePath, int lineNumber, string member)
        {
        }

        /// <inheritdoc/>
        public void Error(string message, string filePath, int lineNumber, string member)
        {
        }

        /// <inheritdoc/>
        public void Error(Exception exception, string message, string filePath, int lineNumber, string member)
        {
        }
    }
}
