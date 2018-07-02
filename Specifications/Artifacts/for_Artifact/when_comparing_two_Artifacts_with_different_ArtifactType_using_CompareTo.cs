/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
 
using Machine.Specifications;

namespace Dolittle.Artifacts.Specs.for_Artifact
{
    public class when_comparing_two_Artifacts_with_different_ArtifactType_using_CompareTo : given.artifacts_with_different_ArtifactType
    {
        static bool result;

        Because of = () => result = artifactA.CompareTo(artifactB) == 0;

        It should_not_be_considered_the_same = () => result.ShouldBeFalse();
    }
}