/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="ISubFeature"/>
    /// </summary>
    public class SubFeature : ISubFeature
    {
        List<IApplicationLocationSegment> _subFeatures = new List<IApplicationLocationSegment>();

        /// <summary>
        /// Initializes a new instance of <see cref="SubFeature"/>
        /// </summary>
        /// <param name="parent">Parent <see cref="Feature"/></param>
        /// <param name="featureName">Name of the <see cref="IFeature"/></param>
        public SubFeature(IFeature parent, FeatureName featureName)
        {
            Name = featureName;
            Parent = parent;
            parent.AddSubFeature(this);
        }

        /// <inheritdoc/>
        public FeatureName Name { get; }

        /// <inheritdoc/>
        public IApplicationLocationSegment Parent { get; }

        /// <inheritdoc/>
        public IEnumerable<IApplicationLocationSegment> Children => _subFeatures;

        /// <inheritdoc/>
        public void AddSubFeature(ISubFeature subFeature)
        {
            ThrowIfSubFeatureAlradyAdded(subFeature);
            _subFeatures.Add(subFeature);
        }

        /// <inheritdoc/>
        public void AddChild(IApplicationLocationSegment child)
        {
            _subFeatures.Add(child);
        }
        

        void ThrowIfSubFeatureAlradyAdded(ISubFeature subFeature)
        {
            if (_subFeatures.Contains(subFeature)) throw new SubFeatureAlreadyAddedToFeature(this, subFeature);
        }

        IApplicationLocationSegmentName IApplicationLocationSegment.Name => Name;
    }
}
