/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dolittle.Collections;
using Dolittle.Reflection;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationStructureFragment"/>
    /// </summary>
    public class ApplicationStructureFragment : IApplicationStructureFragment
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureFragment"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> of <see cref="IApplicationLocation"/> this fragment represents</param>
        /// <param name="required">Whether or not the fragment is required - default false</param>
        /// <param name="recursive">Whether or not the fragment can appear recursively - default false</param>
        public ApplicationStructureFragment(Type type, bool required = false, bool recursive = false): this(type, NullApplicationStructureFragment.Instance, new IApplicationStructureFragment[0], required, recursive) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureFragment"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> of <see cref="IApplicationLocation"/> this fragment represents</param>
        /// <param name="children">Child <see cref="IApplicationStructureFragment">fragments</see></param>
        /// <param name="required">Whether or not the fragment is required - default false</param>
        /// <param name="recursive">Whether or not the fragment can appear recursively - default false</param>
        public ApplicationStructureFragment(
            Type type,
            IEnumerable<IApplicationStructureFragment> children,
            bool required = false,
            bool recursive = false): this(type, NullApplicationStructureFragment.Instance, children, required, recursive) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureFragment"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> of <see cref="IApplicationLocation"/> this fragment represents</param>
        /// <param name="parent">The <see cref="IApplicationStructureFragment">parent fragment</see></param>
        /// <param name="required">Whether or not the fragment is required - default false</param>
        /// <param name="recursive">Whether or not the fragment can appear recursively - default false</param>
        public ApplicationStructureFragment(
            Type type,
            IApplicationStructureFragment parent,
            bool required = false,
            bool recursive = false): this(type, parent, new IApplicationStructureFragment[0], required, recursive) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureFragment"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> of <see cref="IApplicationLocation"/> this fragment represents</param>
        /// <param name="parent">The <see cref="IApplicationStructureFragment">parent fragment</see></param>
        /// <param name="children">Child <see cref="IApplicationStructureFragment">fragments</see></param>
        /// <param name="required">Whether or not the fragment is required - default false</param>
        /// <param name="recursive">Whether or not the fragment can appear recursively - default false</param>
        public ApplicationStructureFragment(
            Type type,
            IApplicationStructureFragment parent,
            IEnumerable<IApplicationStructureFragment> children,
            bool required = false,
            bool recursive = false)
        {
            ThrowIfTypeIsNotApplicationLocation(type);
            ThrowIfTypeDoesNotHaveADefaultConstructorTakingNameAndPossiblyParent(type);

            if (parent != null && parent.GetType()!= typeof(NullApplicationStructureFragment))
            {
                ThrowIfTypeDoesNotBelongToParent(type, parent.Type);
                ThrowIfParentCannotHoldType(parent.Type, type);
            }

            ThrowIfAnyChildDoesNotBelongToType(children, type);
            ThrowIfTypeCannotHoldAnyOfTheChildren(type, children);
            Type = type;
            Parent = parent;
            Required = required;
            Recursive = recursive;
            Children = children;
        }

        /// <inheritdoc/>
        public Type Type { get; }

        /// <inheritdoc/>
        public bool Required { get; }

        /// <inheritdoc/>
        public IApplicationStructureFragment Parent { get; }

        /// <inheritdoc/>
        public bool HasParent => Parent != null && Parent.GetType()!= typeof(NullApplicationStructureFragment);

        /// <inheritdoc/>
        public IEnumerable<IApplicationStructureFragment> Children { get; }

        /// <inheritdoc/>
        public bool Recursive { get; }

        void ThrowIfTypeIsNotApplicationLocation(Type type)
        {
            if (!typeof(IApplicationLocationSegment).IsAssignableFrom(type))throw new ApplicationStructureFragmentMustBeApplicationLocation(type);
        }

        void ThrowIfTypeDoesNotHaveADefaultConstructorTakingNameAndPossiblyParent(Type type)
        {
            var nameParameterType = typeof(string);
            var valid = true;
            var constructor = type.GetConstructors().SingleOrDefault();
            if (constructor == null)valid = false;
            else
            {
                var parameters = constructor.GetParameters();
                if (parameters.Length == 0 || parameters.Length > 2)valid = false;
                else
                {
                    ParameterInfo nameParameter;
                    ParameterInfo parentParameter = null;
                    if (parameters.Length == 1)nameParameter = parameters[0];
                    else
                    {
                        parentParameter = parameters.SingleOrDefault(parameter => typeof(IApplicationLocationSegment).IsAssignableFrom(parameter.ParameterType));
                        if( parentParameter == null ) throw new InvalidConstructorForApplicationLocationSegment(type, nameParameterType);
                        
                        nameParameter = parameters.Single(parameter => parameter != parentParameter);
                    }

                    if (type.ImplementsOpenGeneric(typeof(IApplicationLocationSegment<>)))
                    {
                        nameParameterType = type.GetInterfaces().Single(i => i.Name == typeof(IApplicationLocationSegment<>).Name).GenericTypeArguments[0];
                    }

                    if (nameParameter.ParameterType != nameParameterType) valid = false;
                    if (parameters.Length == 2 && parentParameter == null) valid = false;
                }
            }

            if (!valid)throw new InvalidConstructorForApplicationLocationSegment(type, nameParameterType);
        }

        void ThrowIfParentCannotHoldType(Type parent, Type type)
        {
            var canHoldType = typeof(ICanHoldApplicationLocationSegmentsOfType<>);
            var interfaces = parent.GetInterfaces().Where(i => i.GUID == canHoldType.GUID);
            if( !interfaces.Any(i => i.GenericTypeArguments[0].IsAssignableFrom(type)) ) 
                throw new ParentApplicationStructureFragmentMustBeAbleToHoldType(parent, type);
        }

        void ThrowIfTypeDoesNotBelongToParent(Type type, Type parent)
        {
            var belongToType = typeof(IBelongToAnApplicationLocationSegmentTypeOf<>);
            var interfaces = type.GetInterfaces().Where(i => i.GUID == belongToType.GUID);

            if( !interfaces.Any(i => i.GenericTypeArguments[0].IsAssignableFrom(parent)) ) 
                throw new ApplicationStructureFragmentMustBelongToParent(parent, type);
        }

        void ThrowIfTypeCannotHoldAnyOfTheChildren(Type type, IEnumerable<IApplicationStructureFragment> children)
        {
            children.ForEach(fragment => ThrowIfParentCannotHoldType(type, fragment.Type));
        }

        void ThrowIfAnyChildDoesNotBelongToType(IEnumerable<IApplicationStructureFragment> children, Type type)
        {
            children.ForEach(fragment => ThrowIfTypeDoesNotBelongToParent(fragment.Type, type));
        }
    }
}