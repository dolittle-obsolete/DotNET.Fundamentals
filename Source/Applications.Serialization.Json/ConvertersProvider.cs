/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Newtonsoft.Json;
using Dolittle.Serialization.Json;
using Dolittle.Applications;

namespace Dolittle.Applications.Serialization.Json
{
    /// <summary>
    /// Provides converters related to concepts for Json serialization purposes
    /// </summary>
    public class ConvertersProvider : ICanProvideConverters
    {
        readonly IApplicationArtifactIdentifierStringConverter _applicationArtifactIdentifierStringConverter;

        /// <summary>
        /// Initializes a new instance 
        /// </summary>
        /// <param name="applicationArtifactIdentifierStringConverter"><see cref="IApplicationArtifactIdentifierStringConverter"/> for converting to and from <see cref="IApplicationArtifactIdentifier"/></param>
        public ConvertersProvider(IApplicationArtifactIdentifierStringConverter applicationArtifactIdentifierStringConverter)
        {
            _applicationArtifactIdentifierStringConverter = applicationArtifactIdentifierStringConverter;
        }

        /// <inheritdoc/>
        public IEnumerable<JsonConverter> Provide()
        {
            return new JsonConverter[] {
                new ApplicationArtifactIdentifierJsonConverter(_applicationArtifactIdentifierStringConverter)
            };
        }
    }
}