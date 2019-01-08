/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 * --------------------------------------------------------------------------------------------*/

using Dolittle.Concepts;

namespace Dolittle.PropertyBags.for_PropertyBag.for_Migrations
{
    public class ComplexType : Value<ComplexType>
    {
        public ComplexType(string first, int second)
        {
            MyFirstProperty = first;
            MySecondProperty = second;
        }

        public string MyFirstProperty { get; }
        public int MySecondProperty { get; }
    }
}