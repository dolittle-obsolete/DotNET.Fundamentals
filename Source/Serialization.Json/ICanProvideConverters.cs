/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dolittle.Serialization.Json
{
    /// <summary>
    /// Defines an interface capable of providing <see cref="JsonConverter"/> instances
    /// </summary>
    public interface ICanProvideConverters
    {
        /// <summary>
        /// Provide collection of <see cref="JsonConverter">converters</see>
        /// </summary>
        /// <returns><see cref="IEnumerable{JsonConverter}">Converters</see> provided</returns>
        IEnumerable<JsonConverter> Provide();
    }
}