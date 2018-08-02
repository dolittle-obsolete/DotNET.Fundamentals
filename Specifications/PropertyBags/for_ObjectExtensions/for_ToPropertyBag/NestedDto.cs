using System;
using Dolittle.Concepts;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectExtensions.for_ToPropertyBag
{

    internal class NestedDto : SimpleDto
    {   
        public SimpleDto Child { get; set; } 
    }
}