using System.Collections.ObjectModel;
using Machine.Specifications;

namespace Dolittle.Reflection.for_DictionaryTypeExtensions
{
    public class when_checking_readonly_dictionary_if_is_dictionary
    {
        static bool result;

        Because of = () => result = typeof(ReadOnlyDictionary<string,string>).IsDictionary();

        It should_be_considered_a_dictionary = () => result.ShouldBeTrue();
    }
}