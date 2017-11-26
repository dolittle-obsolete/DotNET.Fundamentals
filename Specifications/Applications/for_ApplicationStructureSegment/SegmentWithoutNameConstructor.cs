using System;

namespace doLittle.Applications.for_ApplicationStructureFragment
{
    public class SegmentWithoutNameConstructor : IApplicationLocationSegment
    {
        public SegmentWithoutNameConstructor(int i)
        {

        }
        
        public IApplicationLocationSegmentName Name => throw new NotImplementedException();
    }
}