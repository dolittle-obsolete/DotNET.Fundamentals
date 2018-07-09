using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_BoundedContext
{
    public class when_creating
    {
        static BoundedContextName name = new BoundedContextName{Value = "Some bounded context"};
        static BoundedContext bounded_context;

        Because of = () => bounded_context = new BoundedContext(name);

        It should_have_the_name = () => ((string)bounded_context.Name.AsString()).ShouldEqual(name.Value);
    }
}
