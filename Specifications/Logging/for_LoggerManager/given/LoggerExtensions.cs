// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Logging;
using Dolittle.Logging.Internal;

namespace Dolittle.Specs.Logging.for_LoggerManager.given
{
    public static class LoggerExtensions
    {
        public static ILogMessageWriter[] GetLogMessageWriters(this ILogger logger)
        {
            var internalLogger = logger as InternalLogger;
            return internalLogger?.LogMessageWriters;
        }
    }
}