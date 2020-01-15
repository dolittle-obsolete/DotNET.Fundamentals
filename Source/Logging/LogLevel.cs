﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Logging
{
    /// <summary>
    /// The severity of a log entry.
    /// </summary>
    /// <remarks>
    /// Inspired by : https://docs.microsoft.com/en-us/aspnet/core/api/microsoft.extensions.logging.loglevel.
    /// </remarks>
    public enum LogLevel
    {
        /// <summary>
        /// Trace message
        /// </summary>
        Trace,

        /// <summary>
        /// Informational message
        /// </summary>
        Debug,

        /// <summary>
        /// Informational message
        /// </summary>
        Info,

        /// <summary>
        /// Warning message
        /// </summary>
        Warning,

        /// <summary>
        /// Critical message
        /// </summary>
        Critical,

        /// <summary>
        /// Error message
        /// </summary>
        Error
    }
}
