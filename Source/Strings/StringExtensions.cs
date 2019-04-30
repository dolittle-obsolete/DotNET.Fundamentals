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
        const string _redCode = "31m";
        const string _yellowCode = "33m";
        const string _whiteCode = "37m";
        const string _resetCode = "0m";
        

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

        /// <summary>
        /// Prepend color coding for Red - typically used in CLI and such
        /// </summary>
        /// <param name="string">string to prepend to</param>
        /// <returns>String with prepended color coding</returns>
        public static string Red(this string @string)
        {
            return _colorPrepend + _redCode + @string + _colorPrepend + _resetCode;
        }

        /// <summary>
        /// Prepend color coding for Yellow - typically used in CLI and such
        /// </summary>
        /// <param name="string">string to prepend to</param>
        /// <returns>String with prepended color coding</returns>
        public static string Yellow(this string @string)
        {
            return _colorPrepend + _yellowCode + @string + _colorPrepend + _resetCode;
        }


        /// <summary>
        /// Prepend color coding for White - typically used in CLI and such
        /// </summary>
        /// <param name="string">string to prepend to</param>
        /// <returns>String with prepended color coding</returns>
        public static string White(this string @string)
        {
            return _colorPrepend + _whiteCode + @string + _colorPrepend + _resetCode;
        }        
    }
}
