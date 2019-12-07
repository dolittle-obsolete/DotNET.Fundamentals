// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.RegularExpressions;

namespace Dolittle.PropertyBags.Migrations
{
    /// <summary>
    /// A very basic property name validator (currently based on C#).
    /// </summary>
    /// <remarks>
    /// Regex taken from https://stackoverflow.com/questions/33711381/regular-expression-to-check-valid-property-name-in-c-sharp?rq=1.
    /// </remarks>
    public static class PropertyNameValidator
    {
        static readonly Regex IsIdentifier = new Regex(@"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)*$", RegexOptions.Compiled);

        /// <summary>
        /// Indicates if the property name is a valid identifier.
        /// </summary>
        /// <param name="propertyName">String to validate.</param>
        /// <returns>true if valid, false if invalid.</returns>
        public static bool IsValid(string propertyName)
        {
            return IsIdentifier.IsMatch(propertyName);
        }
    }
}