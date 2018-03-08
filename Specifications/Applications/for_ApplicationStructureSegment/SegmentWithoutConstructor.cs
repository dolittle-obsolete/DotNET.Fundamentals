using System;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{
    public class SegmentWithoutConstructor : IApplicationLocationSegment
    {
        
        public IApplicationLocationSegmentName Name => throw new NotImplementedException();
    }
}