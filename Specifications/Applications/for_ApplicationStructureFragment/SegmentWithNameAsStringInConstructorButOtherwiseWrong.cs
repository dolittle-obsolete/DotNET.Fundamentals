using System.Collections.Generic;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{
    public class SegmentWithNameAsStringInConstructorButOtherwiseWrong : IApplicationLocationSegment
    {
        public SegmentWithNameAsStringInConstructorButOtherwiseWrong(string name, int parent)
        {

        }
        
        public IApplicationLocationSegmentName Name => throw new System.NotImplementedException();

        public Segment Parent => throw new System.NotImplementedException();

        public IEnumerable<IApplicationLocationSegment> Children => throw new System.NotImplementedException();

        public void AddChild(IApplicationLocationSegment child)
        {
            throw new System.NotImplementedException();
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