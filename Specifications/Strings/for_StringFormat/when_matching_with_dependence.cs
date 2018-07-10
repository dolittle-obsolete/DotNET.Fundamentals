using System.Linq;
using Dolittle.Strings;
using Machine.Specifications;

namespace Dolittle.Specs.Strings.for_StringFormat
{
    public class when_matching_with_dependence
    {
        const string bounded_context = "BoundedContext";
        const string module = "Module";
        const string feature = "Feature";
        const string sub_feature1 = "SubFeature1";
        const string sub_feature2 = "SubFeature2";

        static string to_match = $"Domain.{bounded_context}.{module}.{feature}.{sub_feature1}.{sub_feature2}";
        static IStringFormat string_format;

        static ISegmentMatches matches;
        Establish context = () =>
        {
            string_format = StringFormat.Parse("[.]Domain.^{BoundedContext}.^{Module}.-^{Feature}.-^{SubFeature}*");
        };

        Because of = () =>  matches = string_format.Match(to_match);

        It should_have_5_matches = () => matches.Count().ShouldEqual(5);
    }
}