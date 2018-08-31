using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace Dolittle.PropertyBags.Specs.for_ObjectExtensions.for_ToPropertyBag
{
    public class when_converting_a_dto_with_enumerable
    {
        static DtoWithEnumerableSimple dto_with_enumerable_of_string;
        static PropertyBag dto_with_enumerable_of_string_result;
        Establish context = () =>
        {
            dto_with_enumerable_of_string = new DtoWithEnumerableSimple(){StringList = new List<string>{"element1", "element2"}};
        };
        Because of = () => 
        {
            dto_with_enumerable_of_string_result = dto_with_enumerable_of_string.ToPropertyBag();
        };

        It dto_with_enumerable_of_string_result_should_not_be_null = () => dto_with_enumerable_of_string_result.ShouldNotBeNull();
        It should_have_key = () => 
        {
            dto_with_enumerable_of_string_result.ContainsKey($"{nameof(dto_with_enumerable_of_string.StringList)}").ShouldBeTrue();
        };
        It should_be_an_array_of_strings = () =>
        {
            var list = (dto_with_enumerable_of_string_result[nameof(dto_with_enumerable_of_string.StringList)] as IEnumerable<object>).ToArray();
            for(int i = 0; i < list.Length; i++)
            {
                list[i].ShouldEqual(dto_with_enumerable_of_string.StringList.ToArray()[i]);
            }
        };
    }
}