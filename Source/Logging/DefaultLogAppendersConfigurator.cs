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
        readonly GetCurrentLoggingContext _getCurrentLoggingContext;
        readonly ILoggerFactory _loggerFactory;
        readonly string _environment;
        /// <summary>
        /// Initializes a new instance of <see cref="DefaultLogAppendersConfigurator"/>
        /// </summary>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> to use</param>
        /// <param name="getCurrentLoggingContext"></param>
        /// <param name="environment"></param>
        public DefaultLogAppendersConfigurator(ILoggerFactory loggerFactory, GetCurrentLoggingContext getCurrentLoggingContext, string environment)
        {
            _loggerFactory = loggerFactory;
            _getCurrentLoggingContext = getCurrentLoggingContext;
            _environment = environment;
        }

        /// <inheritdoc/>
        public void Configure(ILogAppenders appenders)
        {
            if (_environment == "Production") appenders.Add(new Json.JsonLogAppender(_getCurrentLoggingContext));
            else appenders.Add(new DefaultLogAppender(_getCurrentLoggingContext, _loggerFactory));
        }
    }
}
