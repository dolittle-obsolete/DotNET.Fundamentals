using System;
using System.Collections.Generic;

namespace doLittle.Applications.for_ApplicationStructureFragment
{
    public class FragmentWithoutBelonging : IApplicationLocationFragment
    {
        public IApplicationLocationFragmentName Name => throw new NotImplementedException();
    }
}