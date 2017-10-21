/*---------------------------------------------------------------------------------------------
*  Copyright (c) 2008-2017 doLittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using doLittle.Collections;
using doLittle.Reflection;

namespace doLittle.Logging
{
    /// <summary>
    /// Represents the entrypoint for configuring logging and the <see cref="ILogAppenders"/>
    /// </summary>
    public class LoggingConfigurator
    {
        /// <summary>
        /// Discover any <see cref="ICanConfigureLogAppenders"/> from the entry assembly and setup 
        /// <see cref="ILogAppenders"/>
        /// </summary>
        /// <returns>An instance of <see cref="ILogAppenders"/> that can be used</returns>
        public static ILogAppenders DiscoverAndConfigure(ILoggerFactory loggerFactory)
        {
            var assembly = Assembly.GetEntryAssembly();
            var types = assembly.GetTypes();

            var configuratorTypes = types.Where(t => t.HasInterface<ICanConfigureLogAppenders>());

            var configurators = new List<ICanConfigureLogAppenders>();
            configurators.Add(new DefaultLogAppendersConfigurator(loggerFactory));
            configurators.AddRange(configuratorTypes.Select(c =>
            {
                if (!c.HasDefaultConstructor()) throw new LogAppenderConfiguratorMissingDefaultConstructor(c);
                return Activator.CreateInstance(c) as ICanConfigureLogAppenders;
            }));

            var logAppenders = new LogAppenders(configurators);
            return logAppenders;
        }
    }
}
