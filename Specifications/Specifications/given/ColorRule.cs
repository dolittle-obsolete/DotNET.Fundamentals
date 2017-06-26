/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Specifications.Specs.given
{
    public class ColorRule : Specification<ColoredShape>
    {
        readonly string _Color;

        public ColorRule(string matchingColor)
        {
            _Color = matchingColor;
            Predicate = shape => shape.Color == _Color;
        }
    }
}