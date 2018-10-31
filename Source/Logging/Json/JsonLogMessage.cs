/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Logging.Json
{
    /// <summary>
    /// Defines the log message for the <see cref="JsonLogAppender"/>
    /// </summary>
    public class JsonLogMessage
    {
        /// <summary>
        /// Instantiates an instance of <see cref="JsonLogMessage"/>
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="timestamp"></param>
        /// <param name="application"></param>
        /// <param name="boundedContext"></param>
        /// <param name="tenant"></param>
        /// <param name="correlationId"></param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        /// <param name="member"></param>
        /// <param name="message"></param>
        /// <param name="stackTrace"></param>
        public JsonLogMessage(string logLevel, DateTimeOffset timestamp, Guid application, Guid boundedContext, Guid tenant, Guid correlationId, string filePath, int lineNumber, string member, string message, string stackTrace) 
        {
            LogLevel = logLevel;
            Timestamp = timestamp;
            Application = application;
            BoundedContext = boundedContext;
            Tenant = tenant;
            CorrelationId = correlationId;
            FilePath = filePath;
            LineNumber = lineNumber;
            Member = member;
            Message = message;
            StackTrace = stackTrace;
        }
        /// <summary>
        /// The level of severity of the logging
        /// </summary>
        /// <value></value>
        public string LogLevel {get;}
        /// <summary>
        /// The timestamp of when the logging occurred
        /// </summary>
        public DateTimeOffset Timestamp {get; }
        /// <summary>
        /// The Application Id
        /// </summary>
        public Guid Application {get;}
        /// <summary>
        /// The BoundedContext Id
        /// </summary>
        public Guid BoundedContext {get;}
        /// <summary>
        /// The Tenant Id
        /// </summary>
        public Guid Tenant {get;}
        /// <summary>
        /// The Correlation Id
        /// </summary>
        public Guid CorrelationId {get;}
        /// <summary>
        /// The Filepath source of the log message
        /// </summary>
        public string FilePath {get;}
        /// <summary>
        /// The line number of the source of the log message
        /// </summary>
        public int LineNumber {get;}
        /// <summary>
        /// The member of the source of the log message
        /// </summary>
        public string Member {get;}
        /// <summary>
        /// The log message
        /// </summary>
        public string Message {get;}
        /// <summary>
        /// The exception's stacktrace
        /// </summary>
        /// <value></value>
        public string StackTrace {get;}

    }
}