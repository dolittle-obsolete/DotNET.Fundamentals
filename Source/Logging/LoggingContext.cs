/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Logging
{
    /// <summary>
    /// Defines a delegate which retrieves the <see cref="LoggingContext"/>
    /// </summary>
    public delegate LoggingContext GetCurrentLoggingContext(); 
    /// <summary>
    /// The context of the logging
    /// </summary>
    public class LoggingContext
    {
        
        /// <summary>
        /// The Application Id
        /// </summary>
        public Guid Application {get; set;}
        /// <summary>
        /// The BoundedContext Id
        /// </summary>
        public Guid BoundedContext {get; set;}
        /// <summary>
        /// The Tenant Id
        /// </summary>
        public Guid TenantId {get; set;}
        /// <summary>
        /// The environment of the process that logged this message
        /// </summary>
        /// <value></value>
        public string Environment {get; set;}
        /// <summary>
        /// The Correlation Id
        /// </summary>
        public Guid CorrelationId {get; set;}
    }
}