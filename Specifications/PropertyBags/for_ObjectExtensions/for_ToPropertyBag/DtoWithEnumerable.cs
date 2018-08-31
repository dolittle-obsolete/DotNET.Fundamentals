using System.Collections.Generic;

namespace Dolittle.PropertyBags.Specs.for_ObjectExtensions.for_ToPropertyBag
{
    public class DtoWithEnumerableSimple
    {
        public IEnumerable<string> StringList {get; set;} = new List<string>();
    }
}