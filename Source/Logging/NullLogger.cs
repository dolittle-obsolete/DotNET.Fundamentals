// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Logging
{
    /// <summary>
    /// Represents an implementation of <see cref="ILogger"/> that does nothing.
    /// </summary>
    /// <typeparam name="T">The type that the log messages relate to.</typeparam>
    internal class NullLogger<T> : ILogger<T>
    {
        /// <summary>
        /// Gets the static singleton instance of <see cref="NullLogger{T}"/>.
        /// </summary>
        public static ILogger<T> Instance { get; } = new NullLogger<T>();

        /// <inheritdoc/>
        public void Trace(string message, params object[] args)
        {
        }

        /// <inheritdoc/>
        public void Trace(Exception exception, string message, params object[] args)
        {
        }

        /// <inheritdoc/>
        public void Debug(string message, params object[] args)
        {
        }

        /// <inheritdoc/>
        public void Debug(Exception exception, string message, params object[] args)
        {
        }

        /// <inheritdoc/>
        public void Information(string message, params object[] args)
        {
        }

        /// <inheritdoc/>
        public void Information(Exception exception, string message, params object[] args)
        {
        }

        /// <inheritdoc/>
        public void Warning(string message, params object[] args)
        {
        }

        /// <inheritdoc/>
        public void Warning(Exception exception, string message, params object[] args)
        {
        }

        /// <inheritdoc/>
        public void Critical(string message, params object[] args)
        {
        }

        /// <inheritdoc/>
        public void Critical(Exception exception, string message, params object[] args)
        {
        }

        /// <inheritdoc/>
        public void Error(string message, params object[] args)
        {
        }

        /// <inheritdoc/>
        public void Error(Exception exception, string message, params object[] args)
        {
        }
    }
}
