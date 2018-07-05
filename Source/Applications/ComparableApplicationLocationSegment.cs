using Dolittle.Reflection;
using Dolittle.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents the base implementation of an <see cref="IApplicationLocationSegment"/>
    /// </summary>
    public abstract class ComparableApplicationLocationSegment : IApplicationLocationSegment
    {
        List<IApplicationLocationSegment> _children = new List<IApplicationLocationSegment>();

        /// <inheritdoc/>
        public IApplicationLocationSegmentName Name {get; }
        
        /// <inheritdoc/>
        public IEnumerable<IApplicationLocationSegment> Children => _children;

        /// <summary>
        /// Initializes an instance of <see cref="ComparableApplicationLocationSegment"/>
        /// </summary>
        public ComparableApplicationLocationSegment(IApplicationLocationSegmentName name) 
        {
            Name = name;
        }

        /// <inheritdoc/>
        public void AddChild(IApplicationLocationSegment child)
        {
            _children.Add(child);
        }

        /// <inheritdoc/>
        public int CompareTo(object obj)
        {
            return GetHashCode().CompareTo(obj.GetHashCode());
        }

        /// <inheritdoc/>
        public int CompareTo(IApplicationLocationSegment other)
        {
            return GetHashCode().CompareTo(other.GetHashCode());
        }

        /// <inheritdoc/>
        public bool Equals(IApplicationLocationSegment other)
        {
            if (Name.AsString() != other.Name.AsString()) return false;

            if (! DontHaveChildren(this, other)) return false;

            if (! HaveChildren(this, other)) return false;
            
            if (HaveChildren(this, other)) 
            {
                if (Children.Count() != other.Children.Count()) return false;

                foreach (var child in Children)
                {
                    if (! other.Children.Any(otherChild => child.Equals(otherChild))) return false;
                }
            }
            return true;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is IApplicationLocationSegment other)
                return Equals(other);
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = Name.AsString().GetHashCode();

            if (HaveChildren(this))
            {
                Children.ForEach(child => hashCode += child.GetHashCode());
            }
            return hashCode;
        }

        bool HaveChildren(params IApplicationLocationSegment[] segments)
        {
            foreach (var segment in segments) 
            {
                if (segment.Children.Count() == 0) return false;
            }
            return true;
        }
        bool DontHaveChildren(params IApplicationLocationSegment[] segments)
        {
             foreach (var segment in segments) 
            {
                if (segment.Children.Count() > 1) return false;
            }
            return true;
        }
        
    }
}