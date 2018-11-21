using Machine.Specifications;

namespace Dolittle.Immutability.for_ImmutableExtensions
{
    public class when_checking_for_immutability_with_class_with_properties_with_setters
    {
        static bool result;

        Because of = () => result = typeof(class_with_properties_with_setters).IsImmutable();

        It should_be_considered_mutable = () => result.ShouldBeFalse();
    }
}