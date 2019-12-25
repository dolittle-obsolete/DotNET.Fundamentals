// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using System.Text;
using Dolittle.Strings;

namespace Dolittle.Logging.Json
{
    /// <summary>
    /// Defines extension methods for working with <see cref="JsonLogMessage"/>.
    /// </summary>
    public static class JsonLogMessageExtensions
    {
        static readonly PropertyInfo[] _props = typeof(JsonLogMessage).GetProperties();
        static StringBuilder _sb;

        /// <summary>
        /// Converts a <see cref="JsonLogMessage"/> to a json string.
        /// </summary>
        /// <param name="jsonLogMessage"><see cref="JsonLogMessage"/> to convert.</param>
        /// <returns>String representation.</returns>
        public static string ToJson(this JsonLogMessage jsonLogMessage)
        {
            _sb = new StringBuilder();
            WriteJsonLogMessage(jsonLogMessage);
            return _sb.ToString();
        }

        static void WriteJsonLogMessage(JsonLogMessage jsonLogMessage)
        {
            _sb.Append("{");

            for (int i = 0; i < _props.Length; i++)
            {
                var prop = _props[i];
                var propName = prop.Name;
                var value = prop.GetValue(jsonLogMessage);
                if (i == _props.Length - 1) WriteProperty(propName, value);
                else WriteProperty(propName, value, true);
            }

            _sb.Append("}");
        }

        static void WriteProperty(string propName, object value, bool requiresComma = false)
        {
            _sb.Append($"\"{propName.ToCamelCase()}\": ");
            WriteObject(value);
            if (requiresComma) _sb.Append(", ");
        }

        static void WriteObject(object obj)
        {
            if (obj == null)
                WriteNull();
            else if (obj is string x)
                WriteString(x);
            else if (obj is Guid guid)
                WriteGuid(guid);
            else if (obj is int x2)
                WriteInt(x2);
            else if (obj is DateTimeOffset dateTimeOffset)
                WriteDateTimeOffset(dateTimeOffset);
        }

        static void WriteNull()
        {
            _sb.Append("null");
        }

        static void WriteString(string obj)
        {
            _sb.Append(System.Web.HttpUtility.JavaScriptStringEncode(obj, addDoubleQuotes: true));
        }

        static void WriteGuid(Guid id)
        {
            _sb.Append($"\"{id}\"");
        }

        static void WriteInt(int obj)
        {
            _sb.Append(obj);
        }

        static void WriteDateTimeOffset(DateTimeOffset timestamp)
        {
            _sb.Append($"\"{timestamp}\"");
        }
    }
}