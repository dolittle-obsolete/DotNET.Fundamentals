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
        /// <param name="module">Owning <see cref="Module"/></param>
        /// <param name="name"><see cref="FeatureName">Name</see> of the feature</param>
        public Feature(IModule module, FeatureName name)
        {
            Parent = module;
            Name = name;
            module.AddFeature(this);
        }

        /// <inheritdoc/>
        public FeatureName Name { get; }

        /// <inheritdoc/>
        public IModule Parent { get; }

        /// <inheritdoc/>
        public IEnumerable<IApplicationLocationSegment> Children => _subFeatures;

        /// <inheritdoc/>
        public void AddSubFeature(ISubFeature subFeature)
        {
            ThrowIfSubFeatureAlradyAdded(subFeature);
            _subFeatures.Add(subFeature);
        }

        void ThrowIfSubFeatureAlradyAdded(ISubFeature subFeature)
        {
            if (_subFeatures.Contains(subFeature)) throw new SubFeatureAlreadyAddedToFeature(this, subFeature);
        }

        IApplicationLocationSegmentName IApplicationLocationSegment.Name => Name;
    }
}
