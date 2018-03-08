/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.Specifications.Specs.for_And
{
    [Subject(typeof(Specification<>))]
    public class when_applying_an_and_rule_against_a_instance_satifying_only_one_part : given.rules_and_colored_shapes
    {
        static bool is_satisfied;

        Because of = () => is_satisfied = squares.And(green).IsSatisfiedBy(red_square);

        It should_not_be_satisfied = () => is_satisfied.ShouldBeFalse();
    }
}