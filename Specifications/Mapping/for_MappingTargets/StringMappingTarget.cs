using System.Reflection;
using doLittle.Mapping;

namespace doLittle.Mapping.Specs.for_MappingTargets
{
    public class StringMappingTarget : MappingTargetFor<string>
    {
        protected override void SetValue(string target, MemberInfo member, object value)
        {
            
        }
    }
}
