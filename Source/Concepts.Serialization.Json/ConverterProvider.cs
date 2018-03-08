/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Newtonsoft.Json;
using Dolittle.Serialization.Json;

namespace Dolittle.Concepts.Serialization.Json
{
    /// <summary>
    /// Provides converters related to concepts for Json serialization purposes
    /// </summary>
    public class ConverterProvider : ICanProvideConverters
    {
        /// <inheritdoc/>
        public IEnumerable<JsonConverter> Provide()
        {
            return new JsonConverter[] {
                new ConceptConverter(),
                new ConceptDictionaryConverter()
            };
        }
    }
}