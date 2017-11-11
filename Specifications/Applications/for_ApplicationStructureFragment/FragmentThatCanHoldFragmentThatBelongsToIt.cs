using System;
using System.Collections.Generic;

namespace doLittle.Applications.for_ApplicationStructureFragment
{
    public class FragmentThatCanHoldFragmentThatBelongsToIt :
        IApplicationLocationFragment,
        ICanHoldApplicationLocationFragmentsOfType<FragmentBelongingToFragmentThatCanHoldIt>
    {
        public IApplicationLocationFragmentName Name => throw new NotImplementedException();

        public IEnumerable<FragmentBelongingToFragmentThatCanHoldIt> Children => throw new NotImplementedException();
    }
}