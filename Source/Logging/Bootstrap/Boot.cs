// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Collections;
using Dolittle.Reflection;

namespace Dolittle.Logging.Bootstrap
{
    /// <summary>
    /// Represents the entrypoint for initializating Logging.
    /// </summary>
    public static class Boot
    {
        /// <summary>
        /// Discover any <see cref="ICanConfigureLogAppenders"/> from the entry assembly and setup
        /// <see cref="ILogAppenders"/>.
        /// </summary>
        /// <param name="defaultLogAppender"><see cref="ILogAppender"/> to use, if any - default is null.</param>
        /// <param name="entryAssembly">The entry <see cref="Assembly"/>.</param>
        /// <returns>An instance of <see cref="ILogAppenders"/> that can be used.</returns>
        public static ILogAppenders Start(
             ILogAppender defaultLogAppender = null,
             Assembly entryAssembly = null)
        {
            if (entryAssembly == null) entryAssembly = Assembly.GetEntryAssembly();
            var types = entryAssembly.GetTypes();

            var configuratorTypes = types.Where(t => t.HasInterface<ICanConfigureLogAppenders>());

            var configurators = new List<ICanConfigureLogAppenders>();
            configuratorTypes.ForEach(c =>
            {
                ThrowIfLogAppenderConfiguratorIsMissingDefaultConstructor(c);
                var configurator = Activator.CreateInstance(c) as ICanConfigureLogAppenders;
                configurators.Add(configurator);
            });

            return new LogAppenders(configurators, defaultLogAppender);
        }

        static void ThrowIfLogAppenderConfiguratorIsMissingDefaultConstructor(Type c)
        {
            if (!c.HasDefaultConstructor()) throw new LogAppenderConfiguratorMissingDefaultConstructor(c);
        }
    }
}