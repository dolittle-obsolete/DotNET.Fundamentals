/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/


namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Defines a builder for building <see cref="PropertyDescription"/>
    /// </summary>
    public interface IPropertyDescriptionBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        PropertyDescription Build();
    }
}