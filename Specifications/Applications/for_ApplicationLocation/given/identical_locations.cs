/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Applications;
using Machine.Specifications;

namespace Applications.for_ApplicationLocation.given
{
    public class identical_locations
    {
        protected static ApplicationLocation locationA;
        protected static ApplicationLocation locationB;

        Establish context = () => {
            locationA = new ApplicationLocation(GetSegments());
            locationB = new ApplicationLocation(GetSegments());
        };

        static IEnumerable<IApplicationLocationSegment> GetSegments()
        {
            var boundedContext = new BoundedContext(new BoundedContextName{Value = "The Bounded Context"});
            var module = new Module(boundedContext, new ModuleName{Value = "The Module"});
            var feature = new Feature(module, new FeatureName{Value = "The Feature"});

            return new IApplicationLocationSegment[] { 
                boundedContext,
                module, 
                feature
            };
        }
    }
}