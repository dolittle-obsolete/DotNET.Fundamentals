using Machine.Specifications;

namespace Dolittle.Dynamic.Specs.for_WriteOnceExpandoObject.given
{
    public class a_write_once_expando_object_without_values
    {
        protected static dynamic values;

        Establish context = () => values = new WriteOnceExpandoObject(d => { });
    }
}
