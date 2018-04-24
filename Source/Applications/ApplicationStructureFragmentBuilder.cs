/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationStructureFragmentBuilder"/>
    /// </summary>
    public class ApplicationStructureFragmentBuilder : IApplicationStructureFragmentBuilder
    {
        readonly IApplicationStructureFragment _fragment;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureFragmentBuilder"/>
        /// </summary>
        /// <param name="fragment"><see cref="IApplicationStructureFragment">Current fragment state</see></param>
        internal ApplicationStructureFragmentBuilder(IApplicationStructureFragment fragment)
        {
            _fragment = fragment;
        }

        /// <inheritdoc/>
        public IApplicationStructureFragment Fragment => _fragment;

        /// <inheritdoc/>
        public IApplicationStructureFragmentBuilder Required
        {
            get
            {
                return new ApplicationStructureFragmentBuilder(
                    new ApplicationStructureFragment(
                        _fragment.Type,
                        _fragment.Parent,
                        _fragment.Children,
                        true,
                        _fragment.Recursive
                    )
                );
            }
        }

        /// <inheritdoc/>
        public IApplicationStructureFragmentBuilder Recursive
        {
            get
            {
                return new ApplicationStructureFragmentBuilder(
                    new ApplicationStructureFragment(
                        _fragment.Type,
                        _fragment.Parent,
                        _fragment.Children,
                        _fragment.Required,
                        _fragment.Recursive
                    )
                );
            }
        }

        /// <inheritdoc/>
        public IApplicationStructureFragmentBuilder WithChild<T>() where T : IApplicationLocationSegment
        {
            return WithChild<T>(_ => _);
        }   

        /// <inheritdoc/>
        public IApplicationStructureFragmentBuilder WithChild<T>(Func<IApplicationStructureFragmentBuilder, IApplicationStructureFragmentBuilder> callback)where T : IApplicationLocationSegment
        {
            var childFragment = new ApplicationStructureFragment(typeof(T), Fragment, false, false);
            IApplicationStructureFragmentBuilder childBuilder = new ApplicationStructureFragmentBuilder(childFragment);
            childBuilder = callback(childBuilder);
           var children = new List<IApplicationStructureFragment>(_fragment.Children);
            children.Add(childFragment);
            return new ApplicationStructureFragmentBuilder(
                new ApplicationStructureFragment(
                    _fragment.Type,
                    _fragment.Parent,
                    children,
                    _fragment.Required,
                    _fragment.Recursive
                )
            );
        }
    }
}