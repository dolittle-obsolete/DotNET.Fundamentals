/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Newtonsoft.Json;
using Dolittle.Serialization.Json;
using Dolittle.Logging;

namespace Dolittle.Concepts.Serialization.Json
{
    /// <summary>
    /// Provides converters related to concepts for Json serialization purposes
    /// </summary>
    public class ConverterProvider : ICanProvideConverters
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Instantiates an instance of <see cref="ConverterProvider" />
        /// </summary>
        /// <param name="logger">A logger</param>
        public ConverterProvider(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public IEnumerable<JsonConverter> Provide()
        {
            return new JsonConverter[] {
                new ConceptConverter(),
                new ConceptDictionaryConverter(_logger)
            };
        }
    }
}