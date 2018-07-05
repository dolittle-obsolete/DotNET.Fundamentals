/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 
using Machine.Specifications;

namespace Dolittle.Artifacts.Specs.for_Artifact
{
    public class when_comparing_two_instances_representing_the_same_Artifact_using_CompareTo : given.same_artifacts
    {
        static bool result;

        Because of = () => result = artifactA.CompareTo(artifactB) == 0;

        It should_be_considered_the_same = () => result.ShouldBeTrue();
    }
}