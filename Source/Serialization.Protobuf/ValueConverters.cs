/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Collections;
using Dolittle.Types;

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Represents an implementation of <see cref="IValueConverters"/>
    /// </summary>
    public class ValueConverters : IValueConverters
    {
        readonly IInstancesOf<IValueConverter> _converters;

        /// <summary>
        /// Initializes a new instance of <see cref="ValueConverters"/>
        /// </summary>
        /// <param name="converters"></param>
        public ValueConverters(IInstancesOf<IValueConverter> converters)
        {
            _converters = converters;
        }

        /// <inheritdoc/>
        public bool CanConvert(Type type)
        {
            return _converters.Any(_ => _.CanConvert(type));
        }

        /// <inheritdoc/>
        public IValueConverter GetConverterFor(Type type)
        {
            var valueConverter = _converters.FirstOrDefault(_ => _.CanConvert(type));
            ThrowIfMissingValueConverter(type, valueConverter);
            return valueConverter;
        }

        void ThrowIfMissingValueConverter(Type type, IValueConverter valueConverter)
        {
            if (valueConverter == null) throw new MissingValueConverter(type);
        }
    }
}