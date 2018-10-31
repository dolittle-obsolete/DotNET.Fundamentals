/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using System.Text;
using Dolittle.Strings;

namespace Dolittle.Logging.Json
{
    /// <summary>
    /// Defines extension methods for working with <see cref="JsonLogMessage"/>
    /// </summary>
    public static class JsonLogMessageExtensions
    {
        readonly static PropertyInfo[] _props = typeof(JsonLogMessage).GetProperties();
        static StringBuilder _sb;
        
        /// <summary>
        /// Converts a <see cref="JsonLogMessage"/> to a json string
        /// </summary>
        /// <param name="jsonLogMessage"></param>
        /// <returns></returns>
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
			_sb.AppendFormat("\"{0}\": ", propName.ToCamelCase());
            WriteObject(value);
            if (requiresComma) _sb.Append(", ");
        }
        static void WriteObject(object obj, bool requiresComma = false)
        {
            if (obj == null)
				WriteNull();
			else if (obj is string)
				WriteString((string)obj);
			else if (obj is Guid)
				WriteGuid((Guid)obj);
			else if (obj is int)
				WriteInt((int)obj);
            else if (obj is DateTimeOffset)
                WriteDateTimeOffset((DateTimeOffset)obj);
        }
        static void WriteNull()
		{
		    _sb.Append("null");
		}
        static void WriteString(string obj)
        {
            _sb.AppendFormat("\"{0}\"", obj.Replace(@"\", "" ).Replace("\"", "\\\""));
        }
        static void WriteGuid(Guid obj)
        {
            _sb.AppendFormat("\"{0}\"", obj.ToString());
        }
        static void WriteInt(int obj)
        {
            _sb.Append(obj);
        }
        static void WriteDateTimeOffset(DateTimeOffset obj)
        {
            _sb.AppendFormat("\"{0}\"", obj.ToString());
        }
    }
}