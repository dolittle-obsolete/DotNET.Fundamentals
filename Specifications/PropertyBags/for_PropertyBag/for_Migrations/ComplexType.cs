using Dolittle.Concepts;

namespace Dolittle.PropertyBags.for_PropertyBag.for_Migrations
{

    public class ComplexType : Value<ComplexType>
    {
        public ComplexType(string first, int second)
        {
            MyFirstProperty = first;
            MySecondProperty = second;
        }

        public string MyFirstProperty { get; }
        public int MySecondProperty { get; }
    }
}