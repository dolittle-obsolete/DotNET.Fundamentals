using System;
using Machine.Specifications;

namespace Dolittle.Applications.Specs.for_ApplicationBuilder
{
    public class when_building_with_SubFeature_followed_by_Feature : given.an_ApplicationBuilder
    {
        static Exception result;
        Because of = () => 
            result = Catch.Exception(() => application_builder
                .WithStructureStartingWith<SubFeature>(sf =>
                    sf.WithChild<Feature>())
                .Build());
        
        It should_throw_ApplicationStructureFragmentMustBelongToParent = () => result.ShouldBeOfExactType(typeof(ApplicationStructureFragmentMustBelongToParent));
    }
}