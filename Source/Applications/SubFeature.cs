/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="ISubFeature"/>
    /// </summary>
    public class SubFeature : ComparableApplicationLocationSegment,
        ISubFeature
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SubFeature"/>
        /// </summary>
        /// <param name="parent">Parent <see cref="Feature"/></param>
        /// <param name="featureName">Name of the <see cref="IFeature"/></param>
        public SubFeature(IFeature parent, FeatureName featureName) : base(featureName)
        {
            Name = featureName;
            Parent = parent;
            parent.AddSubFeature(this);
        }

        /// <inheritdoc/>
        new public FeatureName Name { get; }

        IApplicationLocationSegmentName IApplicationLocationSegment.Name => Name;

        /// <inheritdoc/>
        public IApplicationLocationSegment Parent { get; }


        /// <inheritdoc/>
        public void AddSubFeature(ISubFeature subFeature)
        {
            ThrowIfSubFeatureAlreadyAdded(subFeature);
            AddChild(subFeature);
        }

        void ThrowIfSubFeatureAlreadyAdded(ISubFeature subFeature)
        {
            if (Children.Contains(subFeature)) throw new SubFeatureAlreadyAddedToFeature(this, subFeature);
        }
    }
}
