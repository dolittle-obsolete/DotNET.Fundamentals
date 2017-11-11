namespace doLittle.Applications.for_ApplicationStructureFragment
{
    public class FragmentWithBelonging : 
        IApplicationLocationFragment, 
        IBelongToAnApplicationLocationFragmentTypeOf<Fragment>
    {
        public IApplicationLocationFragmentName Name => throw new System.NotImplementedException();

        public Fragment Parent => throw new System.NotImplementedException();
    }
}