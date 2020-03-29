// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Logging
{
    /// <summary>
    /// The logger used when the source type cannot be determined.
    /// </summary>
    internal class UnknownLogger : InternalLogger<UnknownLogMessageSource>
    {
        const string _messagePrefix = "The following log message was written by a logger whose source could not be determined. " +
                                      "This is probably because the source instance was not created using the normal container. " +
                                      "To fix this either 1) use the normal container to create instances of the type that wrote this message, or " +
                                      "2) explicitly specify the source type in the logger e.g. ILogger<SourceType>. " +
                                      "Original message: ";

        protected override void Write(LogLevel logLevel, string message, params object[] args)
            => base.Write(logLevel, _messagePrefix + message, args);

        protected override void Write(LogLevel logLevel, Exception exception, string message, params object[] args)
            => base.Write(logLevel, exception, _messagePrefix + message, args);
    }
}
