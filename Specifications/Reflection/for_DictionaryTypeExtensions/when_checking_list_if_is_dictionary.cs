using System.Collections.Generic;
using Machine.Specifications;

namespace Dolittle.Reflection.for_DictionaryTypeExtensions
{
    public class when_checking_list_if_is_dictionary
    {
        static bool result;

        Because of = () => result = typeof(List<string>).IsDictionary();

        It should_not_be_considered_a_dictionary = () => result.ShouldBeFalse();
    }
}