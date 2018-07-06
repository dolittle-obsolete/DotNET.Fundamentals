using System;
using Dolittle.Concepts;
using Dolittle.Dynamic;
using Machine.Specifications;

namespace Dolittle.Dynamic.for_ObjectExtensions.for_ToPropertyBag
{

    internal class NestedDto : SimpleDto
    {   
        public SimpleDto Child { get; set; } 
    }
}