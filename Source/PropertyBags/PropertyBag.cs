/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Collections;
using Dolittle.Dynamic;

namespace Dolittle.PropertyBags
{
    /// <summary>
    /// An immutable property bag of key-value pairs
    /// </summary>
    public class PropertyBag : WriteOnceExpandoObject, IEquatable<PropertyBag>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PropertyBag"/>
        /// </summary>
        /// <param name="values"><see cref="NullFreeDictionary{TKey,TValue}"/> of values</param>
        public PropertyBag(NullFreeDictionary<string,object> values) : base(values)
        {
        }

        /// <summary>
        /// Equates to object to check if they are equal.null  This operates on "value" equality rather than reference equality.
        /// Two PropertyBags with the same properties and the same values for these properties will be considered equal.
        /// </summary>
        /// <param name="other">The PropertyBag to equate to this</param>
        /// <returns>True if the two PropertyBag instances are equal, false is they
        ///  are not</returns>
        public bool Equals(PropertyBag other)
        {
            if(other == null)
                return false;

            var thisDictionary = this.AsDictionary();
            var otherAsDictionary = other.AsDictionary();

            return  thisDictionary.Count == otherAsDictionary.Count
                        && thisDictionary.Keys.All(key => otherAsDictionary.ContainsKey(key) 
                                                                && thisDictionary[key].Equals(otherAsDictionary[key]));
        }

        /// <summary>
        /// Equates to object to check if they are equal.null  This operates on "value" equality rather than reference equality.
        /// Two PropertyBags with the same properties and the same values for these properties will be considered equal.
        /// </summary>
        /// <param name="obj">The object to equate to this</param>
        /// <returns>True if the objects are equal, false is they
        ///  are not</returns>
        public override bool Equals(object obj)
        {
            var propBag = obj as PropertyBag;
            return Equals(propBag);
        }

        /// <summary>
        /// Equates two objects to check that they are equal
        /// </summary>
        /// <param name="x">First Value</param>
        /// <param name="y">Second value</param>
        /// <returns>True if the objects are equal, false is they
        ///  are not</returns>
        public static bool operator ==(PropertyBag x, PropertyBag y)
        {
            return ReferenceEquals(x, y) || x.Equals(y);
        }

        /// <summary>
        /// Equates two objects to check that they are not equal
        /// </summary>
        /// <param name="x">First Value</param>
        /// <param name="y">Second value</param>
        /// <returns>True if the objects are not equal, false is they are</returns>
        public static bool operator !=(PropertyBag x, PropertyBag y)
        {
            return !(x == y);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Generate(this.AsDictionary().ToArray());
        }

        static int Generate(params KeyValuePair<string,object>[] parameters)
        {
            //http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
            unchecked
            {
                return parameters
                            .Aggregate(17, (current, param) => current*29 + param.Value.GetHashCode());
            }
        }

        /// <summary>
        /// Converts the PropertyBag to a <see cref="NullFreeDictionary{Tstring,Tobject}" />
        /// </summary>
        /// <returns>Instance of <see cref="NullFreeDictionary{Tstring,Tobject}" /></returns>
        public NullFreeDictionary<string,object> ToNullFreeDictionary()
        {
            var dictionary = new NullFreeDictionary<string,object>();
            this.AsDictionary().ForEach(kvp =>
                {
                    object val = kvp.Value.GetType() == typeof(PropertyBag) ? ((PropertyBag)kvp.Value).ToNullFreeDictionary() : kvp.Value;
                    dictionary.Add(kvp.Key,val);
                }
            );
            return dictionary;
        }

        /// <summary>
        /// Implicitly converts a NullFreeDictionary to a PropertyBag
        /// </summary>
        /// <param name="dictionary">NullFreeDictionary to convert</param>
        public static implicit operator PropertyBag(NullFreeDictionary<string,object> dictionary)
        {
            return new PropertyBag(dictionary);
        }

        /// <summary>
        /// Implicitly converts a PropertyBag to a NullFreeDictionary
        /// </summary>
        /// <param name="propertyBag">PropertyBag to convert</param>
        public static implicit operator NullFreeDictionary<string,object>(PropertyBag propertyBag)
        {
            return propertyBag?.ToNullFreeDictionary() ?? new NullFreeDictionary<string, object>();
        }
    }
}