using System.Collections.ObjectModel;
using Machine.Specifications;

namespace Dolittle.Reflection.for_DictionaryTypeExtensions
{
    public class when_checking_if_readonly_dictionary_is_readonly
    {
        static bool result;

        Because of = () => result = typeof(ReadOnlyDictionary<string,string>).IsReadOnlyDictionary();

        It should_be_considered_a_readonly_dictionary = () => result.ShouldBeTrue();
    }        

}