/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Dolittle.ResourceTypes;

namespace Dolittle.IO.Tenants
{
    /// <summary>
    /// Represents a <see cref="IAmAResourceType">resource type</see> for <see cref="IFiles"/>
    /// </summary>
    public class FilesResourceType : IAmAResourceType
    {
        readonly IEnumerable<Type> _services = new[] { typeof(IFiles)};

        /// <inheritdoc/>
        public ResourceType Name => "files";

        /// <inheritdoc/>
        public IEnumerable<Type> Services => _services;
    }
}