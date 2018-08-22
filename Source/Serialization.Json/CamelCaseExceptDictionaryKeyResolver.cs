using System;
using Newtonsoft.Json.Serialization;

namespace Dolittle.Build.Topology
{
    /// <summary>
    /// Represents a <see cref="CamelCasePropertyNamesContractResolver"/>that ignores the casing of Dictionary keys
    /// </summary>
    public class CamelCaseExceptDictionaryKeyResolver : CamelCasePropertyNamesContractResolver
    {
        /// <inheritdoc/>
        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
        {
            JsonDictionaryContract contract = base.CreateDictionaryContract(objectType);

            contract.DictionaryKeyResolver = propertyName => propertyName;

            return contract;
        }
    }
}