/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationBuilder"/>
    /// </summary>
    public class ApplicationBuilder : IApplicationBuilder
    {
        readonly ApplicationName _name;
        readonly IEnumerable<IApplicationLocationSegment> _prefixes;
        readonly IApplicationStructureBuilder _applicationStructureBuilder;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationBuilder"/>
        /// </summary>
        public ApplicationBuilder(ApplicationName name): this(
            name,
            new IApplicationLocationSegment[0],
            new NullApplicationStructureBuilder()
        ) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationBuilder"/>
        /// </summary>
        /// <param name="name">Name of the <see cref="ApplicationName"/> <see cref="IApplication"/>></param>
        /// <param name="prefixes"><see cref="IApplicationLocationSegment">Application location fragments</see> to be as prefixes</param>
        /// <param name="applicationStructureBuilder">The <see cref="IApplicationStructureBuilder"/> for building the structure</param>
        public ApplicationBuilder(
            ApplicationName name,
            IEnumerable<IApplicationLocationSegment> prefixes,
            IApplicationStructureBuilder applicationStructureBuilder)
        {
            _name = name;
            _prefixes = prefixes;
            _applicationStructureBuilder = applicationStructureBuilder;
        }

        /// <inheritdoc/>
        public IApplicationBuilder PrefixLocationsWith(params IApplicationLocationSegment[] prefixes)
        {
            return new ApplicationBuilder(_name, prefixes, _applicationStructureBuilder);
        }

        /// <inheritdoc/>
        public IApplicationBuilder WithStructureStartingWith<TFragment>(Func<IApplicationStructureFragmentBuilder, IApplicationStructureFragmentBuilder> fragmentBuilderCallback, Action<IApplicationStructureBuilder> structureBuilderCallback=null) where TFragment : IApplicationLocationSegment
        {
            IApplicationStructureFragmentBuilder applicationStructureFragmentBuilder = new ApplicationStructureFragmentBuilder(new ApplicationStructureFragment(typeof(TFragment)));
            applicationStructureFragmentBuilder = fragmentBuilderCallback(applicationStructureFragmentBuilder);
            var applicationStructureBuilder = ApplicationStructureBuilder.WithRoot(applicationStructureFragmentBuilder.Fragment);
            if( structureBuilderCallback != null ) structureBuilderCallback(applicationStructureBuilder);
            return new ApplicationBuilder(_name, _prefixes, applicationStructureBuilder);
        }

        /// <inheritdoc/>
        public IApplication Build()
        {
            var applicationStructure = _applicationStructureBuilder.Build();
            var application = new Application(_name, applicationStructure, _prefixes);
            return application;
        }
    }
}