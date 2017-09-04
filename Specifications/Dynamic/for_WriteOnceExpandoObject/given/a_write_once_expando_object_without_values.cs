using Machine.Specifications;

namespace doLittle.Dynamic.Specs.Execution.for_WriteOnceExpandoObject.given
{
    public class a_write_once_expando_object_without_values
    {
        protected static dynamic values;

        Establish context = () => values = new WriteOnceExpandoObject(d => { });
    }
}
