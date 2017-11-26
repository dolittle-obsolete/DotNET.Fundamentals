using System;
using System.Collections.Generic;

namespace doLittle.Applications.for_ApplicationStructureFragment
{
    public class SegmentWithoutBelonging : IApplicationLocationSegment
    {
        public SegmentWithoutBelonging(string name)
        {
            
        }

        public IApplicationLocationSegmentName Name => throw new NotImplementedException();
    }
}