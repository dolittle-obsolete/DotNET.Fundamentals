/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents a feature within a <see cref="Module"/>
    /// </summary>
    public class Feature : ComparableApplicationLocationSegment,
        IFeature
    {

        /// <summary>
        /// Initializes a new instance of <see cref="Feature"/>
        /// </summary>
        /// <param name="parent">Owning <see cref="IApplicationLocation"/></param>
        /// <param name="name"><see cref="IApplicationLocationSegmentName">Name</see> of the feature</param>
        public Feature(IApplicationLocationSegment parent, IApplicationLocationSegmentName name) : base(name)
        {
            Parent = parent;
            parent.AddChild(this);
        }

        /// <inheritdoc/>
        public new FeatureName Name { get; }

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

        IApplicationLocationSegmentName IApplicationLocationSegment.Name => Name;
    }
}
