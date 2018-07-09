using System;
using System.Collections.Generic;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{
    public class SegmentWithoutBelonging : IApplicationLocationSegment
    {
        public SegmentWithoutBelonging(string name)
        {
            
        }

        public IApplicationLocationSegmentName Name => throw new NotImplementedException();

        public IEnumerable<IApplicationLocationSegment> Children => throw new NotImplementedException();

        public void AddChild(IApplicationLocationSegment child)
        {
            
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(IApplicationLocationSegment other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IApplicationLocationSegment other)
        {
            throw new NotImplementedException();
        }
    }
}