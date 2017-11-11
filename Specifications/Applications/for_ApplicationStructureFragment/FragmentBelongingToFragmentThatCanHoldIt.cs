using System;

namespace doLittle.Applications.for_ApplicationStructureFragment
{
    public class FragmentBelongingToFragmentThatCanHoldIt :
        IApplicationLocationFragment,
        IBelongToAnApplicationLocationFragmentTypeOf<FragmentThatCanHoldFragmentThatBelongsToIt>
    {
        public IApplicationLocationFragmentName Name => throw new NotImplementedException();

        public FragmentThatCanHoldFragmentThatBelongsToIt Parent => throw new NotImplementedException();
    }
}