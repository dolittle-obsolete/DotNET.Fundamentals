using System;

namespace doLittle.Applications.for_ApplicationStructureFragment
{
    public class Segment : IApplicationLocationSegment
    {
        public Segment(string name) 
        {

        }
        
        public IApplicationLocationSegmentName Name => throw new NotImplementedException();
    }
}