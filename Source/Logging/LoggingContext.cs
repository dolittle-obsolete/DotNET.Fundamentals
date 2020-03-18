// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Logging
{
    /// <summary>
    /// The context of the logging.
    /// </summary>
    public class LoggingContext
    {
        /// <summary>
        /// Gets or sets the Application Id.
        /// </summary>
        public Guid Application { get; set; }

        /// <summary>
        /// Gets or sets the Microservice Id.
        /// </summary>
        public Guid Microservice { get; set; }

        /// <summary>
        /// Gets or sets the Tenant Id.
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// Gets or sets the environment of the process that logged this message.
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the Correlation Id.
        /// </summary>
        public Guid CorrelationId { get; set; }
    }
}