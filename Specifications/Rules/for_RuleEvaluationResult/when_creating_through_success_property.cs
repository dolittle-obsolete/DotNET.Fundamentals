/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.Rules.for_RuleEvaluationResult
{
    public class when_creating_through_success_property
    {
        static RuleEvaluationResult result;
        static object instance;

        Establish context = () => instance = new object();

        Because of = () => result = RuleEvaluationResult.Success;

        It should_be_considered_success = () => result.IsSuccess.ShouldBeTrue();
        It should_be_considered_success_through_implicit_operator = () => (result == true).ShouldBeTrue();
    }
}