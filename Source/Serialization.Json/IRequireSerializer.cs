/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dolittle.Serialization.Json
{

    /// <summary>
    /// Indicates that a <see cref="ISerializer" /> is required and can be added as a dependency though the Add method
    /// </summary>
    public interface IRequireSerializer 
    {
        /// <summary>
        /// Adds an instance of an <see cref="ISerializer" />
        /// </summary>
        /// <param name="serializer">Serializer instance</param>
        void Add(ISerializer serializer);
    }
}