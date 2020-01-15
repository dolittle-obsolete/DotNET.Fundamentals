// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Mapping;

namespace Dolittle.Mapping.Specs.for_Map
{
    public class MyMap : Map<Source, Target>
    {
        public PropertyMap<Source, Target> ReturnValueForProperty;

        public MyMap()
        {
            ReturnValueForProperty = Property(s => s.SomeProperty);
        }
    }
}
