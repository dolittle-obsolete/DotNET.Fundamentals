using System;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{
    public class SegmentBelongingToSegmentThatCanHoldIt :
        IApplicationLocationSegment,
        IBelongToAnApplicationLocationSegmentTypeOf<SegmentThatCanHoldSegmentThatBelongsToIt>
    {
        public SegmentBelongingToSegmentThatCanHoldIt(string name)
        {

        }

        public IApplicationLocationSegmentName Name => throw new NotImplementedException();

        public SegmentThatCanHoldSegmentThatBelongsToIt Parent => throw new NotImplementedException();

        IApplicationLocationSegment IBelongToAnApplicationLocationSegmentTypeOf<SegmentThatCanHoldSegmentThatBelongsToIt>.Parent => throw new NotImplementedException();

        public void AddChild(IApplicationLocationSegment child)
        {
            
        }
    }
}