/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Dolittle.Collections
{
    /// <inheritdoc/>
    public class NullFreeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        readonly IDictionary<TKey, TValue> _dict;
        /// <inheritdoc/>
        public TValue this[TKey key] { get {return _dict[key];} set {_dict[key] = value;} }
        /// <inheritdoc/>
        public ICollection<TKey> Keys => _dict.Keys;
        /// <inheritdoc/>
        public ICollection<TValue> Values => _dict.Values;
        /// <inheritdoc/>
        public int Count => _dict.Count;
        /// <inheritdoc/>    
        public bool IsReadOnly => false;
        /// <inheritdoc/>

        public NullFreeDictionary()
        {
            _dict = new Dictionary<TKey, TValue>();
        }
        /// <summary>
        /// Instantiates an instance of NullFreeDictionary
        /// </summary>
        /// <param name="dict"></param>
        public NullFreeDictionary(IDictionary<TKey, TValue> dict)
        {
            _dict = new Dictionary<TKey, TValue>();
            foreach (var keyValue in dict)
                Add(keyValue);
            
        }
        /// <inheritdoc/>
        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (value != null)
                _dict[key] = value;
        }
        /// <inheritdoc/>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }
        /// <inheritdoc/>
        public void Clear()
        {
            _dict.Clear();
        }
        /// <inheritdoc/>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dict.Contains(item);
        }
        /// <inheritdoc/>
        public bool ContainsKey(TKey key)
        {
            return _dict.ContainsKey(key);
        }
        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dict.CopyTo(array, arrayIndex);
        }
        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dict.GetEnumerator();
        }
        /// <inheritdoc/>
        public bool Remove(TKey key)
        {
            return _dict.Remove(key);
        }
        /// <inheritdoc/>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _dict.Remove(item);
        }
        /// <inheritdoc/>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dict.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }
    }
}