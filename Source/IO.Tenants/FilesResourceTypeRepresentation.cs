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
    /// Represents a <see cref="IRepresentAResourceType">resource type representation</see> for <see cref="Files"/>
    /// </summary>
    public class FilesResourceTypeRepresentation : IRepresentAResourceType
    {
        static IDictionary<Type, Type> _bindings = new Dictionary<Type, Type>
        {
            {typeof(IFiles), typeof(Files)},
        };

        /// <inheritdoc/>
        public ResourceType Type => "files";

        /// <inheritdoc/>
        public ResourceTypeImplementation ImplementationName => "Files";

        /// <inheritdoc/>
        public Type ConfigurationObjectType => typeof(FilesConfiguration);

        /// <inheritdoc/>
        public IDictionary<Type, Type> Bindings => _bindings;
    }
}