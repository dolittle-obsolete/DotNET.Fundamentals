﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Logging
{
    /// <summary>
    /// Exception that gets thrown when a <see cref="ICanConfigureLogAppenders"/> implementation does not
    /// provide a default constructor.
    /// </summary>
    /// <remarks>
    /// Due to the fact that we want logging to be enabled as the first step of the configuring Dolittle.
    /// We don't have the IOC container ready and can't therefor provide any dependencies.
    /// </remarks>
    public class LogAppenderConfiguratorMissingDefaultConstructor : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogAppenderConfiguratorMissingDefaultConstructor"/> class.
        /// </summary>
        /// <param name="type">Type of <see cref="ICanConfigureLogAppenders"/> that does not have a default constructor.</param>
        public LogAppenderConfiguratorMissingDefaultConstructor(Type type)
            : base($"Log appender of type '{type.AssemblyQualifiedName} does not have a default constructor")
        {
        }
    }
}
