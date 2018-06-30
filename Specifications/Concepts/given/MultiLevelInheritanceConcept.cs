namespace Dolittle.Specs.Concepts.given
{
    public class MultiLevelInheritanceConcept : InheritingFromLongConcept
    {
        public static implicit operator MultiLevelInheritanceConcept(long value)
        {
            return new MultiLevelInheritanceConcept { Value = value };
        }
    }
}