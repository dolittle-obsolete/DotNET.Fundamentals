using System;
using System.Collections.Generic;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{
    public class SegmentWithoutBelonging : IApplicationLocationSegment
    {
        public SegmentWithoutBelonging(string name)
        {
            
        }

        public IApplicationLocationSegmentName Name => throw new NotImplementedException();

        public void AddChild(IApplicationLocationSegment child)
        {
            
        }
    }
}