using System;
using System.Collections.Generic;
using Dolittle.Concepts;

namespace Dolittle.Applications.for_ApplicationStructureFragment
{
    public class SomeName : ConceptAs<string>, IApplicationLocationSegmentName
    {
        public string AsString()
        {
            return Value;
        }
    }

    public class SegmentWithWrongTypeForNameInConstructor : IApplicationLocationSegment<SomeName>
    {
        public SegmentWithWrongTypeForNameInConstructor(string name)
        {

        }
        
        IApplicationLocationSegmentName IApplicationLocationSegment.Name => Name;

        public SomeName Name => throw new NotImplementedException();

        public IEnumerable<IApplicationLocationSegment> Children => throw new NotImplementedException();

        public void AddChild(IApplicationLocationSegment child)
        {
            
        }

        public bool Equals(IApplicationLocationSegment other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(IApplicationLocationSegment other)
        {
            throw new NotImplementedException();
        }
    }
}