namespace Dolittle.Collections.for_EnumerableEqualityComparer
{
    using Machine.Specifications;
    using Dolittle.Collections;
    using System.Collections.Generic;

    [Subject(typeof(EnumerableEqualityComparer<>))]
    public class when_equating_a_collection_to_null
    {
        static IEnumerable<int> collection;
        static IEqualityComparer<IEnumerable<int>> comparer;
        static bool is_equal;

        Establish context = () => 
        {
            collection = new int[]{1,2,3};
            comparer = new EnumerableEqualityComparer<int>();
        };

        Because of = () => is_equal = comparer.Equals(collection, null);

        It should_not_be_equal = () => is_equal.ShouldBeFalse();
    }
}