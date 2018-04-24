using System;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{
    public class SegmentWithoutNameConstructor : IApplicationLocationSegment
    {
        public SegmentWithoutNameConstructor(int i)
        {

        }
        
        public IApplicationLocationSegmentName Name => throw new NotImplementedException();

        public void AddChild(IApplicationLocationSegment child)
        {
            
        }
    }
}