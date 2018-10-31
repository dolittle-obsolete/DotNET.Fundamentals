/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
#if(!NET461)
using Microsoft.Extensions.Logging;
#endif

namespace Dolittle.Logging
{
    /// <summary>
    /// Represents the default <see cref="ICanConfigureLogAppenders">configurator</see> for <see cref="ILogAppenders"/>
    /// </summary>
    public class DefaultLogAppendersConfigurator : ICanConfigureLogAppenders
    {
        GetCurrentLoggingContext _getCurrentLoggingContext;
#if(!NET461)
        ILoggerFactory _loggerFactory;
        /// <summary>
        /// Initializes a new instance of <see cref="DefaultLogAppendersConfigurator"/>
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> to use</param>
        /// <param name="getCurrentLoggingContext"></param>
        public DefaultLogAppendersConfigurator(ILoggerFactory loggerFactory, GetCurrentLoggingContext getCurrentLoggingContext)
        {
            _loggerFactory = loggerFactory;
            _getCurrentLoggingContext = getCurrentLoggingContext;
        }
#else
        /// <summary>
        /// Initializes a new instance of <see cref="DefaultLogAppendersConfigurator"/>
        /// </summary>
        /// <param name="getCurrentLoggingContext"></param>
        public DefaultLogAppendersConfigurator(GetCurrentLoggingContext getCurrentLoggingContext)
        {
            _getCurrentLoggingContext = getCurrentLoggingContext;
        }
#endif


        /// <inheritdoc/>
        public void Configure(ILogAppenders appenders)
        {
#if (NET461)
            var defaultLogAppender = new DefaultLogAppender(_getCurrentLoggingContext);
#else
            var defaultLogAppender = new DefaultLogAppender(_getCurrentLoggingContext, _loggerFactory);
#endif
            appenders.Add(defaultLogAppender);
            appenders.Add(new Json.JsonLogAppender(_getCurrentLoggingContext));
        }
    }
}
