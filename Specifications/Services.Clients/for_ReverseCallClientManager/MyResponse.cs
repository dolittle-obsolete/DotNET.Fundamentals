// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using contracts::Dolittle.Services.Contracts;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace Dolittle.Services.Clients.for_ReverseCallClientManager
{
    public class MyResponse : IMessage
    {
        public ReverseCallResponseContext ResponseContext { get; set; }

        public MessageDescriptor Descriptor => throw new System.NotImplementedException();

        public int CalculateSize()
        {
            return 0;
        }

        public void MergeFrom(CodedInputStream input)
        {
        }

        public void WriteTo(CodedOutputStream output)
        {
        }
    }
}