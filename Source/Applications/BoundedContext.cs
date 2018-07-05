/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IBoundedContext"/>
    /// </summary>
    public class BoundedContext : ComparableApplicationLocationSegment, 
        IBoundedContext
    {
        IApplicationLocationSegmentName IApplicationLocationSegment.Name => Name;
        BoundedContextName IApplicationLocationSegment<BoundedContextName>.Name => Name.AsString();

        /// <summary>
        /// Initializes a new instance of <see cref="BoundedContext"/>
        /// </summary>
        public BoundedContext(IApplicationLocationSegmentName name) : base(name)
        {
        }

        /// <inheritdoc/>
        public void AddModule(IModule module)
        {
            ThrowIfModuleAlreadyAdded(module);
            AddChild(module);
        }

        /// <inheritdoc/>
        public void AddFeature(IFeature feature)
        {
            ThrowIfFeatureAlreadyAdded(feature);
            AddChild(feature);
        }

        void ThrowIfModuleAlreadyAdded(IModule module)
        {
            if (Children.Contains(module)) throw new ModuleAlreadyAddedToBoundedContext(this, module);
        }

        void ThrowIfFeatureAlreadyAdded(IFeature feature)
        {
            if (Children.Contains(feature)) throw new FeatureAlreadyAddedToBoundedContext(this, feature);
        }
    }
}
