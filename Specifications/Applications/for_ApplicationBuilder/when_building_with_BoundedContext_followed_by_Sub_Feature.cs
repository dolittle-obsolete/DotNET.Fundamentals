using System;
using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_ApplicationBuilder
{
    public class when_building_with_BoundedContext_followed_by_Sub_Feature : given.an_ApplicationBuilder
    {
        static Exception result;
        Because of = () => 
            result = Catch.Exception(() => application_builder
                .WithStructureStartingWith<BoundedContext>(bc =>
                    bc.WithChild<SubFeature>())
                .Build());
        
        It should_throw_ApplicationStructureFragmentMustBelongToParent = () => result.ShouldBeOfExactType(typeof(ParentApplicationStructureFragmentMustBeAbleToHoldType));

    }
}