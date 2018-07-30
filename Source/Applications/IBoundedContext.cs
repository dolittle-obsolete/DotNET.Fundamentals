/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Applications
{
    /// <summary>
    /// Defines a the concept of a bounded context from Domain Driven Design
    /// </summary>
    public interface IBoundedContext : IApplicationLocationSegment<BoundedContextName>, ICanHoldApplicationLocationSegmentsOfType<IModule>, ICanHoldApplicationLocationSegmentsOfType<IFeature>,
        IAmARequiredApplicationLocationSegment
        
    {
        /// <summary>
        /// Adds a <see cref="IModule"/> to the <see cref="BoundedContext"/>
        /// </summary>
        /// <param name="module"><see cref="IModule"/> to add</param>
        void AddModule(IModule module);

        /// <summary>
        /// Adds a <see cref="IFeature"/> to the <see cref="BoundedContext"/>
        /// </summary>
        /// <param name="feature"><see cref="IFeature"/> to add</param>
        void AddFeature(IFeature feature);
    }
}