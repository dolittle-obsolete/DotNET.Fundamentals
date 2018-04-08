/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents a feature within a <see cref="Module"/>
    /// </summary>
    public class Feature : IFeature
    {
        List<IApplicationLocationSegment> _subFeatures = new List<IApplicationLocationSegment>();

        /// <summary>
        /// Initializes a new instance of <see cref="Feature"/>
        /// </summary>
        /// <param name="parent">Owning <see cref="IApplicationLocation"/></param>
        /// <param name="name"><see cref="FeatureName">Name</see> of the feature</param>
        public Feature(IApplicationLocationSegment parent, FeatureName name)
        {
            Parent = parent;
            Name = name;
            parent.AddChild(this);
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
