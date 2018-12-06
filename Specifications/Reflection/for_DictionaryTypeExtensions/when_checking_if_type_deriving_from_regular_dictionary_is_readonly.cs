using System.Collections.Generic;
using Machine.Specifications;

namespace Dolittle.Reflection.for_DictionaryTypeExtensions
{
    public class when_checking_if_type_deriving_from_regular_dictionary_is_readonly
    {
        class derived : Dictionary<string,string> {}
        static bool result;

        Because of = () => result = typeof(derived).IsReadOnlyDictionary();

        It should_not_be_considered_a_readonly_dictionary = () => result.ShouldBeFalse();
    }            
}