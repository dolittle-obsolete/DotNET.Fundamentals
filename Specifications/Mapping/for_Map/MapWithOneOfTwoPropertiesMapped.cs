using doLittle.Mapping;

namespace doLittle.Mapping.Specs.for_Map
{
    public class MapWithOneOfTwoPropertiesMapped : Map<SourceWithTwoProperties, Target>
    {
        public MapWithOneOfTwoPropertiesMapped()
        {
            Property(m => m.FirstProperty).To(t => t.SomeOtherProperty);
        }
    }
}
