using System.Collections.Generic;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{
    public class SegmentWithBelonging : 
        IApplicationLocationSegment, 
        IBelongToAnApplicationLocationSegmentTypeOf<Segment>
    {
        public SegmentWithBelonging(string name)
        {

        }
        
        public IApplicationLocationSegmentName Name => throw new System.NotImplementedException();

        public Segment Parent => throw new System.NotImplementedException();

        public IEnumerable<IApplicationLocationSegment> Children => throw new System.NotImplementedException();

        IApplicationLocationSegment IBelongToAnApplicationLocationSegmentTypeOf<Segment>.Parent => throw new System.NotImplementedException();

        public void AddChild(IApplicationLocationSegment child)
        {
            
        }

        public int CompareTo(object obj)
        {
            throw new System.NotImplementedException();
        }

        public int CompareTo(IApplicationLocationSegment other)
        {
            throw new System.NotImplementedException();
        }

        public bool Equals(IApplicationLocationSegment other)
        {
            throw new System.NotImplementedException();
        }
    }
}