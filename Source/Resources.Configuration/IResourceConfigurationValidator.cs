/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace doLittle.Configuration.Resources
{
    /// <summary>
    /// Defines a system that can validate a <see cref="IResourceConfiguration"/>
    /// </summary>
    public interface IResourceConfigurationValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        ResourceConfigurationValidationResult ValidationResult(IResourceConfiguration configuration);
    }
}