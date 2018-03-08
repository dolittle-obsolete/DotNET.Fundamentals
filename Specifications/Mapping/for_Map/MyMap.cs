using Dolittle.Mapping;

namespace Dolittle.Mapping.Specs.for_Map
{
    public class MyMap : Map<Source, Target>
    {
        public PropertyMap<Source, Target> ReturnValueForProperty;

        public MyMap()
        {
            ReturnValueForProperty = Property(s => s.SomeProperty);
        }
    }
}
