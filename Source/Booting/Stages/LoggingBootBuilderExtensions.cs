/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Dolittle.Booting.Stages;
using Dolittle.Logging;
using Microsoft.Extensions.Logging;

namespace Dolittle.Booting
{
    /// <summary>
    /// Extensions for building <see cref="LoggingSettings"/> 
    /// </summary>
    public static class LoggingBootBuilderExtensions
    {
        /// <summary>
        /// Set <see cref="ILogAppender"/> to use
        /// </summary>
        /// <param name="bootBuilder"><see cref="BootBuilder"/> to build</param>
        /// <param name="logAppender"><see cref="ILogAppender"/> to use</param>
        /// <returns>Chained <see cref="BootBuilder"/></returns>
        public static IBootBuilder UseLogAppender(this IBootBuilder bootBuilder, ILogAppender logAppender)
        {
            bootBuilder.Set<LoggingSettings>(_ => _.LogAppender, logAppender);
            return bootBuilder;
        }

        /// <summary>
        /// Set <see cref="ILoggerFactory"/> to use
        /// </summary>
        /// <param name="bootBuilder"><see cref="BootBuilder"/> to build</param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> to use</param>
        /// <returns>Chained <see cref="BootBuilder"/></returns>
        public static IBootBuilder UseLoggerFactory(this IBootBuilder bootBuilder, ILoggerFactory loggerFactory)
        {
            bootBuilder.Set<LoggingSettings>(_ => _.LoggerFactory, loggerFactory);
            return bootBuilder;
        }
    }
}
