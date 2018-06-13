namespace Dolittle.Specs.Concepts.given
{
    public class InheritingFromLongConcept : LongConcept
    {
        public static implicit operator InheritingFromLongConcept(long value)
        {
            return new InheritingFromLongConcept { Value = value };
        }
    }
}