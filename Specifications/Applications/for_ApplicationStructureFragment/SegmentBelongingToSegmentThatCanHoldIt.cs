using System;
using System.Collections.Generic;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{
    public class SegmentBelongingToSegmentThatCanHoldIt:
        IApplicationLocationSegment,
        IBelongToAnApplicationLocationSegmentTypeOf<SegmentThatCanHoldSegmentThatBelongsToIt>
        {
            public SegmentBelongingToSegmentThatCanHoldIt(IApplicationLocationSegment parent, string name)
            {
                Parent = parent;
            }
            public IApplicationLocationSegmentName Name { get; }

            public IApplicationLocationSegment Parent { get; }

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