/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace Dolittle.Dynamic
{
    /// <summary>
    /// Represents an ExpandoObject that can only have values assigned to during creation.
    /// Similar to <see cref="ExpandoObject"/>, members are dynamic and can be added on the fly
    /// </summary>
    public class WriteOnceExpandoObject : DynamicObject, IDictionary<string, object>
    {
        Dictionary<string, object> _actualDictionary = new Dictionary<string, object>();
        bool _construction;

        /// <summary>
        /// Initializes a new instance of <see cref="WriteOnceExpandoObject"/>
        /// </summary>
        /// <param name="populate">Action that gets called during creation for populate the object</param>
        public WriteOnceExpandoObject(Action<dynamic> populate)
        {
            _construction = true;
            populate(this);
            _construction = false;
        }

        /// <inheritdoc/>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this[binder.Name] = value;
            return true; // base.TrySetMember(binder, value);
        }

        /// <inheritdoc/>
        public void Add(string key, object value)
        {
            ThrowIfNotUnderConstruction();
            _actualDictionary.Add(key, value);
        }

        /// <inheritdoc/>
        public bool ContainsKey(string key)
        {
            return _actualDictionary.ContainsKey(key);
        }

        /// <inheritdoc/>
        public ICollection<string> Keys { get { return _actualDictionary.Keys; } }

        /// <inheritdoc/>
        public bool Remove(string key)
        {
            ThrowIfNotUnderConstruction();
            return _actualDictionary.Remove(key);
        }

        /// <inheritdoc/>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _actualDictionary.TryGetValue(binder.Name, out result);
        }

        /// <inheritdoc/>
        public bool TryGetValue(string key, out object value)
        {
            return _actualDictionary.TryGetValue(key, out value);
        }

        /// <inheritdoc/>
        public ICollection<object> Values { get { return _actualDictionary.Values; } }

        /// <inheritdoc/>
        public object this[string key]
        {
            get { return _actualDictionary[key]; }
            set 
            {
                ThrowIfNotUnderConstruction();
                _actualDictionary[key] = value; 
            }
        }

        /// <inheritdoc/>
        public void Add(KeyValuePair<string, object> item)
        {
            ThrowIfNotUnderConstruction();
            _actualDictionary.Add(item.Key, item.Value);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            ThrowIfNotUnderConstruction();
            _actualDictionary.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<string, object> item)
        {
            return _actualDictionary.ContainsKey(item.Key);
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int Count { get { return _actualDictionary.Count; } }

        /// <inheritdoc/>
        public bool IsReadOnly { get { return false; } }

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<string, object> item)
        {
            ThrowIfNotUnderConstruction();
            return _actualDictionary.Remove(item.Key);
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _actualDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _actualDictionary.GetEnumerator();
        }

        void ThrowIfNotUnderConstruction()
        {
            if( !_construction )
                throw new ReadOnlyObjectException();
        }
    }
}
