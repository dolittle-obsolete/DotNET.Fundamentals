using System.Reflection;
using Dolittle.Mapping;

namespace Dolittle.Mapping.Specs.for_MappingTargets
{
    public class StringMappingTarget : MappingTargetFor<string>
    {
        protected override void SetValue(string target, MemberInfo member, object value)
        {
            
        }
    }
}
