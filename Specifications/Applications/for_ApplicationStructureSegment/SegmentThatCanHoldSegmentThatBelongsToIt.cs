using System;
using System.Collections.Generic;

namespace doLittle.Applications.for_ApplicationStructureFragment
{
    public class SegmentThatCanHoldSegmentThatBelongsToIt :
        IApplicationLocationSegment,
        ICanHoldApplicationLocationSegmentsOfType<SegmentBelongingToSegmentThatCanHoldIt>
    {
        public SegmentThatCanHoldSegmentThatBelongsToIt(string name)
        {
            
        }
        
        public IApplicationLocationSegmentName Name => throw new NotImplementedException();

        public IEnumerable<SegmentBelongingToSegmentThatCanHoldIt> Children => throw new NotImplementedException();
    }
}