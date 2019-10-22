/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Strings
{
    /// <summary>
    /// Provides a set of extension methods to <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        const string _colorPrepend = "\x1b[";        

        /// <summary>
        /// Convert a string into a camel cased string
        /// </summary>
        /// <param name="string">string to convert</param>
        /// <returns>Converted string</returns>
        public static string ToCamelCase(this string @string)
        {
            if (!string.IsNullOrEmpty(@string))
            {
                if (@string.Length == 1)
                    return @string.ToLowerInvariant();

                var firstLetter = @string[0].ToString().ToLowerInvariant();
                return firstLetter + @string.Substring(1);
            }
            return @string;
        }

        /// <summary>
        /// Convert a string into a pascal cased string
        /// </summary>
        /// <param name="string">string to convert</param>
        /// <returns>Converted string</returns>
        public static string ToPascalCase(this string @string)
        {
            if (!string.IsNullOrEmpty(@string))
            {
                if (@string.Length == 1)
                    return @string.ToUpperInvariant();

                var firstLetter = @string[0].ToString().ToUpperInvariant();
                return firstLetter + @string.Substring(1);
            }
            return @string;
        }
    }
}
