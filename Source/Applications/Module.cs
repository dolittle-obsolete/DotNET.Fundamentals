/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represent an implementation of <see cref="IModule"/>
    /// </summary>
    public class Module : ComparableApplicationLocationSegment,
        IModule
    {
        /// <inheritdoc/>
        new public ModuleName Name { get; }

        IApplicationLocationSegmentName IApplicationLocationSegment.Name => Name;

        /// <inheritdoc/>
        public IApplicationLocationSegment Parent { get; }
        /// <summary>
        /// Initializes a new instance of <see cref="Module"/>
        /// </summary>
        /// <param name="boundedContext"><see cref="IBoundedContext"/> the <see cref="Module"/> belongs to</param>
        /// <param name="moduleName"><see cref="IApplicationLocationSegmentName">Name</see> of the business component</param>
        public Module(IBoundedContext boundedContext, ModuleName moduleName) : base(moduleName)
        {
            Name = moduleName;
            Parent = boundedContext;
            boundedContext.AddModule(this);
        }

        /// <inheritdoc/>
        public void AddFeature(IFeature feature)
        {
            ThrowIfFeatureIsAlreadyAdded(feature);
            AddChild(feature);
        }

        void ThrowIfFeatureIsAlreadyAdded(IFeature feature)
        {
            if (Children.Contains(feature)) throw new FeatureAlreadyAddedToModule(this, feature);
        }
    }
}
