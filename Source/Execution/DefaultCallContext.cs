/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Execution
{
    /// <summary>
    /// Represents a <see cref="ICallContext"/>
    /// </summary>
    public class DefaultCallContext : ICallContext
    {
        [ThreadStatic]
        static Dictionary<string, object> _data;

        static Dictionary<string, object> Data => _data ?? (_data = new Dictionary<string, object>());

        /// <inheritdoc/>
        public bool HasData(string key)
        {
            return Data.ContainsKey(key);
        }

        /// <inheritdoc/>
        public T GetData<T>(string key)
        {
            return (T)Data[key];
        }

        /// <inheritdoc/>
        public void SetData(string key, object data)
        {
            Data[key] = data;
        }
    }
}
