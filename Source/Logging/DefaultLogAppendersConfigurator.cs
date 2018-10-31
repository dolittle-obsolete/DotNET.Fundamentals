/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
using Microsoft.Extensions.Logging;
namespace Dolittle.Logging
{
    /// <summary>
    /// Represents the default <see cref="ICanConfigureLogAppenders">configurator</see> for <see cref="ILogAppenders"/>
    /// </summary>
    public class DefaultLogAppendersConfigurator : ICanConfigureLogAppenders
    {
        GetCurrentLoggingContext _getCurrentLoggingContext;
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

        /// <inheritdoc/>
        public void Configure(ILogAppenders appenders)
        {
            var defaultLogAppender = new DefaultLogAppender(_getCurrentLoggingContext, _loggerFactory);

            appenders.Add(defaultLogAppender);
            appenders.Add(new Json.JsonLogAppender(_getCurrentLoggingContext));
        }
    }
}
