/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Linq;
using Dolittle.Collections;
using Dolittle.Concepts;
using Google.Protobuf;
using static Google.Protobuf.WireFormat;

namespace Dolittle.Serialization.Protobuf
{
    /// <summary>
    /// Represents an implementation of <see cref="ISerializer"/>
    /// </summary>
    public class Serializer : ISerializer
    {
        readonly IMessageDescriptions _messageDescriptions;
        readonly IValueConverters _valueConverters;

        /// <summary>
        /// Initializes a new instance of <see cref="Serializer"/>
        /// </summary>
        /// <param name="messageDescriptions"><see cref="IMessageDescriptions"/> for <see cref="MessageDescription">descriptions of messages</see></param>
        /// <param name="valueConverters">Available <see cref="IValueConverters"/></param>
        public Serializer(IMessageDescriptions messageDescriptions, IValueConverters valueConverters)
        {
            _messageDescriptions = messageDescriptions;
            _valueConverters = valueConverters;
        }

        /// <inheritdoc/>
        public int GetLengthOf<T>(T instance)
        {
            var messageDescription = _messageDescriptions.GetFor<T>();
            var length = GetLengthOf(instance, messageDescription);
            return length;
        }

        /// <inheritdoc/>
        public T FromProtobuf<T>(Stream stream, bool includesLength = false)
        {
            var instance = (T) Activator.CreateInstance(typeof(T));
            var inputStream = new CodedInputStream(stream);
            var messageDescription = _messageDescriptions.GetFor<T>();

            var tag = inputStream.ReadTag();
            while (!inputStream.IsAtEnd)
            {
                var fieldNumber = WireFormat.GetTagFieldNumber(tag);
                var propertyDescription = messageDescription.Properties.SingleOrDefault(_ => _.Number == fieldNumber);
                if (propertyDescription != null)
                {
                    object value = null;
                    var type = propertyDescription.Property.PropertyType;
                    Type conceptType = null;

                    IValueConverter converter = null;

                    if (_valueConverters.CanConvert(type))
                    {
                        converter = _valueConverters.GetConverterFor(type);
                        type = converter.SerializedAs;

                    }
                    else if (type.IsConcept())
                    {
                        conceptType = type;
                        type = type.GetConceptValueType();
                        
                    }

                    value = ReadValue(inputStream, value, type, converter);

                    if (conceptType != null)
                    {
                        value = ConceptFactory.CreateConceptInstance(conceptType, value);
                    }
                    propertyDescription.Property.SetValue(instance, value);
                }

                tag = inputStream.ReadTag();
            }

            return instance;
        }


        /// <inheritdoc/>
        public T FromProtobuf<T>(byte[] bytes, bool includesLength = false)
        {
            using( var memoryStream = new MemoryStream(bytes) )
            {
                return FromProtobuf<T>(memoryStream, includesLength);
            }
        }

        /// <inheritdoc/>
        public void ToProtobuf<T>(T instance, Stream stream, bool includeLength = false)
        {
            var outputStream = new CodedOutputStream(stream);
            var messageDescription = _messageDescriptions.GetFor<T>();

            if (includeLength)
            {
                var length = GetLengthOf(instance, messageDescription);
                outputStream.WriteLength(length);
            }

            messageDescription.Properties.ForEach(property =>
            {
                var type = property.Property.PropertyType;
                var number = property.Number;
                var value = property.Property.GetValue(instance);

                if (_valueConverters.CanConvert(type))
                {
                    var converter = _valueConverters.GetConverterFor(type);
                    type = converter.SerializedAs;
                    value = converter.ConvertTo(value);
                }
                else if (type.IsConcept())
                {
                    type = type.GetConceptValueType();
                    value = value.GetConceptValue();
                }

                WriteValue(outputStream, type, number, value);
            });
            outputStream.Flush();
        }


        /// <inheritdoc/>
        public byte[] ToProtobuf<T>(T instance, bool includeLength = false)
        {
            using(var stream = new MemoryStream())
            {
                ToProtobuf(instance, stream, includeLength);
                return stream.ToArray();
            }
        }

        object ReadValue(CodedInputStream inputStream, object value, Type type, IValueConverter converter)
        {
            if (type == typeof(Guid))
            {
                var length = inputStream.ReadLength();
                var guidAsBytes = inputStream.ReadBytes();
                value = new Guid(guidAsBytes.ToByteArray());
            }
            else if (type == typeof(string))
            {
                var length = inputStream.ReadLength();
                value = inputStream.ReadString();
            }
            else if (type == typeof(int))
            {
                value = inputStream.ReadInt32();
            }
            else if (type == typeof(Int64))
            {
                value = inputStream.ReadInt64();
            }
            else if (type == typeof(uint))
            {
                value = inputStream.ReadUInt32();
            }
            else if (type == typeof(uint))
            {
                value = inputStream.ReadInt64();
            }
            else if (type == typeof(float))
            {
                value = inputStream.ReadFloat();
            }
            else if (type == typeof(double))
            {
                value = inputStream.ReadDouble();
            }
            else if (type == typeof(bool))
            {
                value = inputStream.ReadBool();
            }
            else if (type == typeof(DateTimeOffset))
            {
                value = DateTimeOffset.FromFileTime(inputStream.ReadInt64());
            }
            else if (type == typeof(DateTime))
            {
                value = DateTime.FromFileTimeUtc(inputStream.ReadInt64());
            }

            if (converter != null) value = converter.ConvertFrom(value);
            return value;
        }

        void WriteValue(CodedOutputStream outputStream, Type type, int number, object value)
        {
            if (type == typeof(Guid))
            {
                outputStream.WriteTag(number, WireType.LengthDelimited);
                var guidAsBytes = ((Guid)value).ToByteArray();
                var byteString = ByteString.CopyFrom(guidAsBytes);
                outputStream.WriteLength(CodedOutputStream.ComputeBytesSize(byteString));
                outputStream.WriteBytes(byteString);
            }
            else if (type == typeof(string))
            {
                var valueAsString = value as string;
                outputStream.WriteTag(number, WireType.LengthDelimited);
                outputStream.WriteLength(CodedOutputStream.ComputeStringSize(valueAsString));
                outputStream.WriteString(valueAsString);
            }
            else if (type == typeof(int))
            {
                outputStream.WriteTag(number, WireType.Varint);
                outputStream.WriteInt32((int)value);
            } 
            else if (type == typeof(Int64))
            {
                outputStream.WriteTag(number, WireType.Varint);
                outputStream.WriteInt64((Int64)value);
            }
            else if (type == typeof(uint))
            {
                outputStream.WriteTag(number, WireType.Varint);
                outputStream.WriteUInt32((uint)value);
            }
            else if (type == typeof(UInt64))
            {
                outputStream.WriteTag(number, WireType.Varint);
                outputStream.WriteUInt64((UInt64)value);
            }
            else if (type == typeof(float))
            {
                outputStream.WriteTag(number, WireType.Varint);
                outputStream.WriteFloat((float)value);
            }
            else if (type == typeof(double))
            {
                outputStream.WriteTag(number, WireType.Varint);
                outputStream.WriteDouble((double)value);
            }
            else if (type == typeof(bool))
            {
                outputStream.WriteTag(number, WireType.Varint);
                outputStream.WriteBool((bool)value);
            }
            else if (type == typeof(DateTimeOffset))
            {
                outputStream.WriteTag(number, WireType.Varint);
                outputStream.WriteInt64(((DateTimeOffset)value).ToFileTime());
            }
            else if (type == typeof(DateTime))
            {
                outputStream.WriteTag(number, WireType.Varint);
                outputStream.WriteInt64(((DateTime)value).ToFileTimeUtc());
            }
        }

        int GetLengthOf<T>(T instance, MessageDescription messageDescription)
        {
            var size = 0;

            messageDescription.Properties.ForEach(property =>
            {
                var type = property.Property.PropertyType;
                var number = property.Number;
                var value = property.Property.GetValue(instance);

                if( _valueConverters.CanConvert(type) )
                {
                    var converter = _valueConverters.GetConverterFor(type);
                    type = converter.SerializedAs;
                    value = converter.ConvertTo(value);
                } else if (type.IsConcept())
                {
                    type = type.GetConceptValueType();
                    value = value.GetConceptValue();
                }

                size += CodedOutputStream.ComputeTagSize(number);
                if (type == typeof(Guid))
                {
                    var guidAsBytes = ((Guid) value).ToByteArray();
                    var length = CodedOutputStream.ComputeBytesSize(ByteString.CopyFrom(guidAsBytes));
                    size += CodedOutputStream.ComputeLengthSize(length);
                    size += length;
                }
                else if (type == typeof(string))
                {
                    var valueAsString = value as string;
                    var length = CodedOutputStream.ComputeStringSize(valueAsString);
                    size += CodedOutputStream.ComputeLengthSize(length);
                    size += length;
                }
                else if (type == typeof(int))
                {
                    size += CodedOutputStream.ComputeInt32Size((int) value);
                }
                else if (type == typeof(Int64))
                {
                    size += CodedOutputStream.ComputeInt64Size((Int64) value);
                }
                else if (type == typeof(uint))
                {
                    size += CodedOutputStream.ComputeUInt32Size((uint) value);
                }
                else if (type == typeof(UInt64))
                {
                    size += CodedOutputStream.ComputeUInt64Size((UInt64) value);
                }
                else if (type == typeof(float))
                {
                    size += CodedOutputStream.ComputeFloatSize((float) value);
                }
                else if (type == typeof(double))
                {
                    size += CodedOutputStream.ComputeDoubleSize((double) value);
                }
                else if (type == typeof(bool))
                {
                    size += CodedOutputStream.ComputeBoolSize((bool) value);
                }
                else if (type == typeof(DateTimeOffset) )
                {
                    size += CodedOutputStream.ComputeInt64Size(((DateTimeOffset)value).ToFileTime());
                }
                else if (type == typeof(DateTime) )
                {
                    size += CodedOutputStream.ComputeInt64Size(((DateTime)value).ToFileTimeUtc());
                }
            });

            return size;
        }
    }
}