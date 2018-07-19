using System;
using System.Linq;

using Dolittle.Reflection;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents the validation strategy for a <see cref="IApplicationStructure"/>
    /// </summary>
    public class DefaultApplicationStructureValidationStrategy : IApplicationStructureValidationStrategy
    {
        /// <inheritdoc/>
        public (bool isValid, InvalidApplicationStructure exception) Validate(IApplicationStructure applicationStructure)
        {
            if (! StartsWithBoundedContext(applicationStructure))
            {
                var innerException = new ApplicationStructureMustStartWithABoundedContext();
                return (false, new InvalidApplicationStructure(innerException));
            }

            var validateStructureFragmentsHaveOnlyOneChildResult = ValidateStructureFragmentsOnlyHaveOneChild(applicationStructure.Root);

            if (! validateStructureFragmentsHaveOnlyOneChildResult.isValid)
            {
                var innerException = new ApplicationStructureFragmentMustNotHaveMoreThanOneChild(validateStructureFragmentsHaveOnlyOneChildResult.structureFragmentType);
                return (false, new InvalidApplicationStructure(innerException));
            }

            var validateRequiredStructureFragmentsResult = ValidateRequiredStructureFragments(applicationStructure.Root);

            if (! validateRequiredStructureFragmentsResult.isValid)
            {
                var innerException = new RequiredStructureFragmentMustBeSetAsRequired(validateRequiredStructureFragmentsResult.structureFragmentType);
                return (false, new InvalidApplicationStructure(innerException));
            }

            var validateRecursiveStructureFragmentsResult = ValidateRecursiveStructureFragments(applicationStructure.Root);

            if (! validateRecursiveStructureFragmentsResult.isValid)
            {
                var innerException = new NonRecursiveStructureFragmentSetAsRecursive(validateRecursiveStructureFragmentsResult.structureFragmentType);
                return (false, new InvalidApplicationStructure(innerException));
            }

            return (true, null);
        }

        bool StartsWithBoundedContext(IApplicationStructure structure)
        {
            return structure.Root != null && structure.Root.Type.HasInterface<IBoundedContext>();
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
                if (StructureFragmentIsRequired(root) && ! root.Required) return (false, root.Type);
                
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
                if (! StructureFragmentCanBeRecursive(root) && root.Recursive) return (false, root.Type);

                if (root.Children.Any())
                {
                    return ValidateRecursiveStructureFragments(root.Children.First());
                }
            }

            return (true, null);
        }

        bool StructureFragmentIsRequired(IApplicationStructureFragment structureFragment)
        {
            return structureFragment.Type != null && structureFragment.Type.HasInterface<IAmARequiredApplicationLocationSegment>();
        }
        bool StructureFragmentCanBeRecursive(IApplicationStructureFragment structureFragment)
        {
            return structureFragment.Type != null && structureFragment.Type.HasInterface<ICanBeARecursiveApplicationLocationSegment>();
        }
    }
}