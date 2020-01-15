// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Mapping;

namespace Dolittle.Mapping.Specs.for_Map
{
    public class MapWithOneOfTwoPropertiesMapped : Map<SourceWithTwoProperties, Target>
    {
        public MapWithOneOfTwoPropertiesMapped()
        {
            Property(m => m.FirstProperty).To(t => t.SomeOtherProperty);
        }
    }
}
