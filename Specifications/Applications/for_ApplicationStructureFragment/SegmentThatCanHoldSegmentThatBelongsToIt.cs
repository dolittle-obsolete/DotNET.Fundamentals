using System;
using System.Collections.Generic;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{
    public class SegmentThatCanHoldSegmentThatBelongsToIt :
        IApplicationLocationSegment,
        ICanHoldApplicationLocationSegmentsOfType<SegmentBelongingToSegmentThatCanHoldIt>
    {
        public SegmentThatCanHoldSegmentThatBelongsToIt(string name)
        {
            
        }
        
        public IApplicationLocationSegmentName Name => throw new NotImplementedException();

        public IEnumerable<IApplicationLocationSegment> Children => throw new NotImplementedException();

        public void AddChild(IApplicationLocationSegment child)
        {
            
        }
    }
}