/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Collections;
using Dolittle.Types;

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Represents an implementation of <see cref="IValueConverters"/>
    /// </summary>
    public class ValueConverters : IValueConverters
    {
        readonly Dictionary<Type, IValueConverter> _converters = new Dictionary<Type, IValueConverter>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converters"></param>
        public ValueConverters(IInstancesOf<IValueConverter> converters)
        {
            converters.ForEach(converter => converter.SupportedTypes.ForEach(type => _converters[type] = converter));
        }

        /// <inheritdoc/>
        public bool CanConvert(Type type)
        {
            return _converters.ContainsKey(type);
        }


        /// <inheritdoc/>
        public IValueConverter GetConverterFor(Type type)
        {
            return _converters[type];
        }
    }
}