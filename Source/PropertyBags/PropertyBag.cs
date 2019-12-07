// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.Dynamic;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// An immutable property bag of key-value pairs.
    /// </summary>
    public class PropertyBag : WriteOnceExpandoObject, IEquatable<PropertyBag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBag"/> class.
        /// </summary>
        /// <param name="values"><see cref="NullFreeDictionary{TKey,TValue}"/> of values.</param>
        public PropertyBag(NullFreeDictionary<string, object> values)
            : base(values)
        {
        }

        /// <summary>
        /// Implicitly converts a NullFreeDictionary to a PropertyBag.
        /// </summary>
        /// <param name="dictionary">NullFreeDictionary to convert.</param>
        public static implicit operator PropertyBag(NullFreeDictionary<string, object> dictionary) => new PropertyBag(dictionary);

        /// <summary>
        /// Implicitly converts a PropertyBag to a NullFreeDictionary.
        /// </summary>
        /// <param name="propertyBag"><see cref="PropertyBag"/> to convert.</param>
        public static implicit operator NullFreeDictionary<string, object>(PropertyBag propertyBag) => propertyBag?.ToNullFreeDictionary() ?? new NullFreeDictionary<string, object>();

        /// <summary>
        /// Equates two objects to check that they are equal.
        /// </summary>
        /// <param name="x">First Value.</param>
        /// <param name="y">Second value.</param>
        /// <returns>
        /// true if the two PropertyBag instances are equal, false is they are not.
        /// </returns>
        public static bool operator ==(PropertyBag x, PropertyBag y) => ReferenceEquals(x, y) || x.Equals(y);

        /// <summary>
        /// Equates two objects to check that they are not equal.
        /// </summary>
        /// <param name="x">First Value.</param>
        /// <param name="y">Second value.</param>
        /// <returns>true if the objects are not equal, false is they are.</returns>
        public static bool operator !=(PropertyBag x, PropertyBag y) => !(x == y);

        /// <summary>
        /// Equates to object to check if they are equal.null  This operates on "value" equality rather than reference equality.
        /// Two PropertyBags with the same properties and the same values for these properties will be considered equal.
        /// </summary>
        /// <param name="other">The PropertyBag to equate to this.</param>
        /// <returns>
        /// true if the two PropertyBag instances are equal, false is they are not.
        /// </returns>
        public bool Equals(PropertyBag other)
        {
            if (other == null)
                return false;

            var thisDictionary = this.AsDictionary();
            var otherAsDictionary = other.AsDictionary();

            return thisDictionary.Count == otherAsDictionary.Count
                        && thisDictionary.Keys.All(key => otherAsDictionary.ContainsKey(key)
                                                                && thisDictionary[key].Equals(otherAsDictionary[key]));
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var propBag = obj as PropertyBag;
            return Equals(propBag);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Generate(this.AsDictionary().ToArray());
        }

        /// <summary>
        /// Converts the PropertyBag to a <see cref="NullFreeDictionary{Tstring,Tobject}"/>.
        /// </summary>
        /// <returns>Instance of <see cref="NullFreeDictionary{Tstring,Tobject}"/>.</returns>
        public NullFreeDictionary<string, object> ToNullFreeDictionary()
        {
            var dictionary = new NullFreeDictionary<string, object>();
            AsDictionary().ForEach(kvp =>
                {
                    object val = kvp.Value.GetType() == typeof(PropertyBag) ? ((PropertyBag)kvp.Value).ToNullFreeDictionary() : kvp.Value;
                    dictionary.Add(kvp.Key, val);
                });

            return dictionary;
        }

        static int Generate(params KeyValuePair<string, object>[] parameters)
        {
            return HashCodeHelper.Generate(parameters);
        }
    }
}