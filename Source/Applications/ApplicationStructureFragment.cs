/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Reflection;
using doLittle.Collections;
using doLittle.Reflection;

namespace doLittle.Applications
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
        public ApplicationStructureFragment(Type type, bool required = false, bool recursive = false)
            : this(type, NullApplicationStructureFragment.Instance, new IApplicationStructureFragment[0], required, recursive)
        {
        }

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
            bool recursive = false)
            : this(type, NullApplicationStructureFragment.Instance, children, required, recursive)
        {
        }

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
            bool recursive = false)
            : this(type, parent, new IApplicationStructureFragment[0], required, recursive)
        {
        }

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

            if (parent != null && parent.GetType() != typeof(NullApplicationStructureFragment))
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
        }

        /// <inheritdoc/>
        public Type Type { get; }

        /// <inheritdoc/>
        public bool Required { get; }

        /// <inheritdoc/>
        public IApplicationStructureFragment Parent { get; }

        /// <inheritdoc/>
        public bool HasParent => Parent != null && Parent.GetType() != typeof(NullApplicationStructureFragment);

        /// <inheritdoc/>
        public IEnumerable<IApplicationStructureFragment> Children { get; }

        /// <inheritdoc/>
        public bool Recursive { get; }

        void ThrowIfTypeIsNotApplicationLocation(Type type)
        {
            if (!typeof(IApplicationLocationFragment).IsAssignableFrom(type)) throw new ApplicationStructureFragmentMustBeApplicationLocation(type);
        }

        void ThrowIfParentCannotHoldType(Type parent, Type type)
        {
            var canHoldType = typeof(ICanHoldApplicationLocationFragmentsOfType<>).MakeGenericType(type);
            if (!canHoldType.IsAssignableFrom(parent)) throw new ParentApplicationStructureFragmentMustBeAbleToHoldType(parent, type);
        }

        void ThrowIfTypeDoesNotBelongToParent(Type type, Type parent)
        {
            var belongToType = typeof(IBelongToAnApplicationLocationFragmentTypeOf<>).MakeGenericType(parent);
            if (!belongToType.IsAssignableFrom(type)) throw new ApplicationStructureFragmentMustBelongToParent(parent, type);
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