namespace Dolittle.PropertyBags.Specs
{
    using Dolittle.Concepts;
    public class CannotBeBuiltByAnyNonUserDefinedFactory : Value<RequiresSpecificConstructionByUser>
    {
        public string Foo { get; }
    }
}