/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.Strings
{
    /// <summary>
    /// Provides a set of extension methods to <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Convert a string into a camel cased string
        /// </summary>
        /// <param name="str">string to convert</param>
        /// <returns>Converted string</returns>
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length == 1)
                    return str.ToLowerInvariant();

                var firstLetter = str[0].ToString().ToLowerInvariant();
                return firstLetter + str.Substring(1);
            }
            return str;
        }

        /// <summary>
        /// Convert a string into a pascal cased string
        /// </summary>
        /// <param name="str">string to convert</param>
        /// <returns>Converted string</returns>
        public static string ToPascalCase(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length == 1)
                    return str.ToUpperInvariant();

                var firstLetter = str[0].ToString().ToUpperInvariant();
                return firstLetter + str.Substring(1);
            }
            return str;
        }
    }
}
