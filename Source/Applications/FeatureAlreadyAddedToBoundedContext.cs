/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Applications
{
    /// <summary>
    /// Exception tat
    /// </summary>
    public class FeatureAlreadyAddedToBoundedContext : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FeatureAlreadyAddedToModule"/>
        /// </summary>
        /// <param name="boundedContext"><see cref="IBoundedContext">Bounded context</see> the feature is already added to</param>
        /// <param name="feature"><see cref="IModule">Module</see> hat is already added</param>
        public FeatureAlreadyAddedToBoundedContext(IBoundedContext boundedContext, IFeature feature) : base($"Feature '{feature.Name}' has already been added to the bounded context '{boundedContext.Name}'") { }
    }
}
