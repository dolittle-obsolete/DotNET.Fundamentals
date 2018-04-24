using System;

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

            public void AddChild(IApplicationLocationSegment child)
            {

            }
        }
}