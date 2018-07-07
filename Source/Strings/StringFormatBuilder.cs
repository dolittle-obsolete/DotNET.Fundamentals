/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dolittle.Strings
{
    /// <summary>
    /// Represents an instance of <see cref="IStringFormatBuilder"/>
    /// </summary>
    public class StringFormatBuilder : IStringFormatBuilder
    {
        char[] _separators;
        IEnumerable<ISegmentBuilder> _segments;

        /// <summary>
        /// Initializes a new instance of <see cref="StringFormatBuilder"/>
        /// </summary>
        /// <param name="separators">Separator characters for segments - must be at least one</param>
        public StringFormatBuilder(params char[] separators)
        {
            ThrowIfMissingSeparators(separators);
            _separators = separators;
            _segments = new ISegmentBuilder[0];
        }

        /// <summary>
        /// Initializes a new instance of <see cref="StringFormatBuilder"/>
        /// </summary>
        /// <param name="segments"><see cref="IEnumerable{ISegment}">Segments</see> to build from</param>
        /// <param name="separators">Separator characters for segments - must be at least one</param>
        public StringFormatBuilder(IEnumerable<ISegmentBuilder> segments, params char[] separators)
        {
            ThrowIfMissingSeparators(separators);
            _separators = separators;
            _segments = segments;
        }

        /// <inheritdoc/>
        public IStringFormat Build()
        {
            UpdateChildren();
            var segmentsWithoutParentDependency = _segments.Where(s => !s.DependsOnPrevious);
            return new StringFormat(segmentsWithoutParentDependency.Select(s => s.Build()), _separators);
        }

        /// <inheritdoc/>
        public IStringFormatBuilder FixedString(string @string)
        {
            return FixedString(@string, f => f);
        }

        /// <inheritdoc/>
        public IStringFormatBuilder FixedString(string @string, Func<IFixedStringSegmentBuilder, IFixedStringSegmentBuilder> callback)
        {
            var parentBuilder = _segments.Count() > 0 ? _segments.Last() : new NullSegmentBuilder();

            IFixedStringSegmentBuilder builder = new FixedStringSegmentBuilder(
                @string,
                parent:parentBuilder,
                children:new ISegmentBuilder[0]
            );

            builder = callback(builder);
            return new StringFormatBuilder(
                _segments.Concat(new[] { builder }),
                _separators
            );
        }

        /// <inheritdoc/>
        public IStringFormatBuilder VariableString(string @string)
        {
            return VariableString(@string, f => f);
        }

        /// <inheritdoc/>
        public IStringFormatBuilder VariableString(string @string, Func<IVariableStringSegmentBuilder, IVariableStringSegmentBuilder> callback)
        {
            var parentBuilder = _segments.Count() > 0 ? _segments.Last() : new NullSegmentBuilder();

            IVariableStringSegmentBuilder builder = new VariableStringSegmentBuilder(
                @string,
                parent: parentBuilder,
                children: new ISegmentBuilder[0]
            );

            builder = callback(builder);
            return new StringFormatBuilder(
                _segments.Concat(new[] { builder }),
                _separators
            );
        }

        /// <summary>
        /// This is just a temporary fix. My opinion is that we need to rethink how we do 
        /// ApplicationStructure and StructureMapping from the ground up again.
        /// 
        /// What we have now is way to generic anc complex
        /// </summary>
        void UpdateChildren()
        {
            var numSegments = _segments.Count();
            var segmentArray = _segments.ToArray();

            for (int i = numSegments - 2; i >= 0; i--)
                if (segmentArray[i].HasChildren)
                    segmentArray[i].NewChild(segmentArray[i+1]);
        }
        void ThrowIfMissingSeparators(char[] separators)
        {
            if (separators.Length == 0) throw new MissingSeparators();
        }
    }
}
