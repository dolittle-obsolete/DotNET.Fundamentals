using System;

namespace doLittle.Applications.for_ApplicationStructureFragment
{
    public class SegmentWithoutConstructor : IApplicationLocationSegment
    {
        
        public IApplicationLocationSegmentName Name => throw new NotImplementedException();
    }
}