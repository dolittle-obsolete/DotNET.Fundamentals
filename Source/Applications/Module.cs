/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represent an implementation of <see cref="IModule"/>
    /// </summary>
    public class Module : IModule
    {
        List<IApplicationLocationSegment> _features = new List<IApplicationLocationSegment>();

        /// <summary>
        /// Initializes a new instance of <see cref="Module"/>
        /// </summary>
        /// <param name="boundedContext"><see cref="IBoundedContext"/> the <see cref="Module"/> belongs to</param>
        /// <param name="moduleName"><see cref="ModuleName">Name</see> of the business component</param>
        public Module(IBoundedContext boundedContext, ModuleName moduleName)
        {
            Parent = boundedContext;
            Name = moduleName;
            boundedContext.AddModule(this);
        }

        /// <inheritdoc/>
        public ModuleName Name { get; }

        /// <inheritdoc/>
        public IApplicationLocationSegment   Parent { get; }

        /// <inheritdoc/>
        public IEnumerable<IApplicationLocationSegment> Children => _features;

        /// <inheritdoc/>
        public void AddFeature(IFeature feature)
        {
            ThrowIfFeatureIsAlreadyAdded(feature);
            _features.Add(feature);
        }

        /// <inheritdoc/>
        public void AddChild(IApplicationLocationSegment child)
        {
            _features.Add(child);
        }
        

        void ThrowIfFeatureIsAlreadyAdded(IFeature feature)
        {
            if (_features.Contains(feature)) throw new FeatureAlreadyAddedToModule(this, feature);
        }

        IApplicationLocationSegmentName IApplicationLocationSegment.Name => Name;
    }
}
