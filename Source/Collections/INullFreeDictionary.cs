
using System.Collections.Generic;

namespace Dolittle.Collections
{
    /// <summary>
    /// Represents a Dictionary that cannot contain null values 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface INullFreeDictionary<TKey,TValue> : IDictionary<TKey, TValue> 
    {
    }
}