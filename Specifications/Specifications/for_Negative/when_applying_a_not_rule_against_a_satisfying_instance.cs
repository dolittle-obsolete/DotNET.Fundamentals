/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.Specifications.Specs.for_Negative
{
    [Subject(typeof(Specification<>))]
    public class when_applying_a_not_rule_against_a_satisfying_instance : given.rules_and_colored_shapes
    {
        static bool is_satisfied;

        Because of = () => is_satisfied = Is.Not(squares).IsSatisfiedBy(green_square);

        It should_be_not_satisfied = () => is_satisfied.ShouldBeFalse();
    }
}