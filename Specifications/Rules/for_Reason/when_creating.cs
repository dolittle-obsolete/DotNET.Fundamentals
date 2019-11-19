using Machine.Specifications;

namespace Dolittle.Rules.Specs.for_Reason
{
    public class when_creating
    {
        static string id = "3847286b-b508-4738-8975-f08383999f5a";
        static string title = "Some Title";
        static string description = "Some description";
        static Reason reason;

        Because of = () => reason = Reason.Create(id,title,description);

        It should_have_the_id_set = () => reason.Id.ToString().ToLowerInvariant().ShouldEqual(id);
        It should_have_title_set = () => reason.Title.ShouldEqual(title);
        It should_have_description_set = () => reason.Description.ShouldEqual(description);
    }
}
