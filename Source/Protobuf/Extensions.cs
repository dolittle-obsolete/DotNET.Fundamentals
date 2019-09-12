/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Grpc.Core;
using Google.Protobuf;

namespace System.Protobuf
{
    /// <summary>
    /// Represents conversion extensions for the common types
    /// </summary>
    public static class Extensions
    {
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
        public static System.Protobuf.guid ToGuid(this System.Guid guid)
        {
            var converted = new System.Protobuf.guid();
            converted.Value = ByteString.CopyFrom(guid.ToByteArray());
            return converted;
        }
    }
}