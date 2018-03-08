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
    }
}