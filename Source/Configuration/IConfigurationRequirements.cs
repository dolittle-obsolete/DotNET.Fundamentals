/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Configuration
{
    /// <summary>
    /// Defines the requirements for a configuration
    /// </summary>
    public interface IConfigurationRequirements
    {
        /// <summary>
        /// 
        /// </summary>
         void Require<T>();

         /// <summary>
         /// 
         /// </summary>
         void Optional<T>();
    }
}