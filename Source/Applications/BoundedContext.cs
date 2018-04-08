/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IBoundedContext"/>
    /// </summary>
    public class BoundedContext : IBoundedContext
    {
        List<IApplicationLocationSegment> _children = new List<IApplicationLocationSegment>();

        /// <summary>
        /// Initializes a new instance of <see cref="BoundedContext"/>
        /// </summary>
        public BoundedContext(BoundedContextName name)
        {
            Name = name;
        }

        /// <inheritdoc/>
        public IEnumerable<IApplicationLocationSegment> Children => _children;


        /// <inheritdoc/>
        public BoundedContextName Name { get; }

        /// <inheritdoc/>
        public void AddModule(IModule module)
        {
            ThrowIfModuleAlreadyAdded(module);
            _children.Add(module);
        }

        /// <inheritdoc/>
        public void AddFeature(IFeature feature)
        {
            ThrowIfFeatureAlreadyAdded(feature);
            _children.Add(feature);
        }

        /// <inheritdoc/>
        public void AddChild(IApplicationLocationSegment child)
        {
            _children.Add(child);
        }

        void ThrowIfModuleAlreadyAdded(IModule module)
        {
            if (_children.Contains(module)) throw new ModuleAlreadyAddedToBoundedContext(this, module);
        }

        void ThrowIfFeatureAlreadyAdded(IFeature feature)
        {
            if (_children.Contains(feature)) throw new FeatureAlreadyAddedToBoundedContext(this, feature);
        }


        IApplicationLocationSegmentName IApplicationLocationSegment.Name => Name;
    }
}
