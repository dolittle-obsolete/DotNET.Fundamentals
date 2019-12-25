// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Mapping
{
    /// <summary>
    /// Defines the strategy for mapping properties.
    /// </summary>
    public interface IPropertyMappingStrategy
    {
        /// <summary>
        /// Performs the mapping.
        /// </summary>
        /// <param name="mappingTarget">Mapping target to use.</param>
        /// <param name="target">Target in which to map to.</param>
        /// <param name="value">Value from to set.</param>
        void Perform(IMappingTarget mappingTarget, object target, object value);
    }
}
