/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationStructure"/>
    /// </summary>
    public class ApplicationStructure : IApplicationStructure
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructure"/>
        /// </summary>
        /// <param name="root"><see cref="IApplicationStructureFragment">Root fragment</see> for the structure</param>
        public ApplicationStructure(IApplicationStructureFragment root)
        {
            Root = root;
        }

        /// <inheritdoc/>
        public IApplicationStructureFragment Root { get; }


        public (bool isValid, InvalidApplicationStructure exception) ValidateStructure()
        {
            if (! StartsWithBoundedContext())
            {
                var innerException = new ApplicationStructureMustStartWithABoundedContext();
                return (false, new InvalidApplicationStructure(innerException));
            }

            var validateStructureFragmentsHaveOnlyOneChildResult = ValidateStructureFragmentsOnlyHaveOneChild(Root);

            if (! validateStructureFragmentsHaveOnlyOneChildResult.isValid)
            {
                var innerException = new ApplicationStructureFragmentMustNotHaveMoreThanOneChild(validateStructureFragmentsHaveOnlyOneChildResult.structureFragmentType);
                return (false, new InvalidApplicationStructure(innerException));
            }

            var validateRequiredStructureFragmentsResult = ValidateRequiredStructureFragments(Root);

            if (! validateRequiredStructureFragmentsResult.isValid)
            {
                var innerException = new RequiredStructureFragmentIsOptional(validateRequiredStructureFragmentsResult.structureFragmentType);
                return (false, new InvalidApplicationStructure(innerException));
            }

            var validateRecursiveStructureFragmentsResult = ValidateRecursiveStructureFragments(Root);

            if (! )

            return (true, null);
        }

        /// <inheritdoc/>
        public int CompareTo(object obj)
        {
            return GetHashCode().CompareTo(obj.GetHashCode());
        }

        /// <inheritdoc/>
        public int CompareTo(IApplicationStructure other)
        {
             return GetHashCode().CompareTo(other.GetHashCode());
        }

        /// <inheritdoc/>
        public bool Equals(IApplicationStructure other)
        {
            return Root.Equals(other.Root);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is IApplicationStructure other)
                return Equals(other);
            return false;
        }

        /// <inheritdoc/>
        public static bool operator ==(ApplicationStructure x, ApplicationStructure y)
        {
            return x.Equals(y);
        }

        /// <inheritdoc/>
        public static bool operator !=(ApplicationStructure x, ApplicationStructure y)
        {
            return !x.Equals(y);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Root.GetHashCode();
        }

        bool StartsWithBoundedContext()
        {
            return Root.Type.IsAssignableFrom(typeof(IBoundedContext));
        }

        (bool isValid, Type structureFragmentType) ValidateStructureFragmentsOnlyHaveOneChild(IApplicationStructureFragment root)
        {
            if (root != null)
            {
                if (root.Children.Any())
                {
                    if (root.Children.Count() > 1) return (false, root.Type);
                    return ValidateStructureFragmentsOnlyHaveOneChild(root.Children.First());
                }
            }
            return (true, null);
        }

        (bool isValid, Type structureFragmentType) ValidateRequiredStructureFragments(IApplicationStructureFragment root)
        {
            if (root != null)
            {
                if (root.MustBeRequired() && ! root.Required) return (false, root.Type);
                
                if (root.Children.Any())
                {
                    return ValidateRequiredStructureFragments(root.Children.First());
                }
            }
            return (true, null);
        }
        (bool isValid, Type structureFragmentType) ValidateRecursiveStructureFragments(IApplicationStructureFragment root)
        {
            if (root != null)
            {
                if (! root.CanBeRecursive() && root.Recursive) return (false, root.Type);

                if (root.Children.Any())
                {
                    return ValidateRecursiveStructureFragments(root.Children.First());
                }
            }

            return (true, null);
        }
    }
}