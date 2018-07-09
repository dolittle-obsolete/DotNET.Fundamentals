using System.Linq;
using Dolittle.Strings;
using Machine.Specifications;

namespace Dolittle.Specs.Strings.for_StringFormatParser
{
    public class when_parsing_a_normal_format : given.a_string_format_parser
    {
        const string first_fixed_segment = "Infrastructure.Events";
        const string first_variable_segment = "Feature";
        const string second_variable_segment = "SubFeature";
        static readonly string format =
        $"[|]{first_fixed_segment}|-^{{{first_variable_segment}}}|-^{{{second_variable_segment}}}*";

        static IStringFormat string_format;
        Because of = () => string_format = parser.Parse(format);

        It should_have_one_segment = () => string_format.Segments.Count().ShouldEqual(1);
        It should_have_root_fixed_segment = () => RootSegment().ToString().ShouldEqual(SegmentToFixedSegmentAsString(first_fixed_segment));
        It should_have_non_optional_root = () => RootSegment().Optional.ShouldBeFalse();
        It should_have_root_with_single_occurrence = () => RootSegment().Occurrences.ShouldEqual(SegmentOccurrence.Single);
        It should_have_a_first_level_child = () => RootSegment().Children.Count().ShouldEqual(1);

        // MOre tests

        static ISegment RootSegment()
        {
            return string_format.Segments.ToArray()[0];
        }
        static ISegment FirstLevelSegment()
        {
            return RootSegment().Children.ToArray()[0];
        }
        static ISegment SecondLevelSegment()
        {
            return FirstLevelSegment().Children.ToArray()[0];
        }
    }
}