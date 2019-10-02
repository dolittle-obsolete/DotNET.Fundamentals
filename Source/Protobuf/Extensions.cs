/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Google.Protobuf;
using Dolittle.Concepts;

namespace Dolittle.Protobuf
{
    /// <summary>
    /// Represents conversion extensions for the common types
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Convert a <see cref="System.Protobuf.guid"/> to a <see cref="ConceptAs{T}"/> of type <see cref="System.Guid"/>
        /// </summary>
        /// <param name="guid"><see cref="System.Protobuf.guid"/> to convert</param>
        /// <returns>Converted <see cref="ConceptAs{T}"/> of type <see cref="System.Guid"/></returns>
        public static T To<T>(this System.Protobuf.guid guid) where T:ConceptAs<System.Guid>, new()
        {
            return new T { Value = new System.Guid(guid.Value.ToByteArray()) };
        }


        /// <summary>
        /// Convert a <see cref="System.Protobuf.guid"/> to <see cref="System.Guid"/>
        /// </summary>
        /// <param name="guid"><see cref="System.Protobuf.guid"/> to convert</param>
        /// <returns>Converted <see cref="System.Guid"/></returns>
        public static System.Guid ToGuid(this System.Protobuf.guid guid)
        {
            return new System.Guid(guid.Value.ToByteArray());
        }

        /// <summary>
        /// Convert a <see cref="System.Guid"/> to <see cref="System.Protobuf.guid"/>
        /// </summary>
        /// <param name="guid"><see cref="System.Guid"/> to convert</param>
        /// <returns>Converted <see cref="System.Protobuf.guid"/></returns>
        public static System.Protobuf.guid ToProtobuf(this System.Guid guid)
        {
            var converted = new System.Protobuf.guid();
            converted.Value = ByteString.CopyFrom(guid.ToByteArray());
            return converted;
        }

        /// <summary>
        /// Convert a <see cref="ConceptAs{T}"/> of type <see cref="System.Guid"/> to <see cref="System.Protobuf.guid"/>
        /// </summary>
        /// <param name="guid"><see cref="ConceptAs{T}"/> of type <see cref="System.Guid"/> to convert</param>
        /// <returns>Converted <see cref="System.Protobuf.guid"/></returns>
        public static System.Protobuf.guid ToProtobuf(this ConceptAs<System.Guid> guid)
        {
            var converted = new System.Protobuf.guid();
            converted.Value = ByteString.CopyFrom(guid.Value.ToByteArray());
            return converted;
        }        
    }
}