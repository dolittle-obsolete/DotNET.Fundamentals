/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
namespace Dolittle.Logging.Json
{

    /// <summary>
    /// Represents a default implementation of <see cref="ILogAppender"/> for using System.Diagnostics.Debug
    /// </summary>
    public class JsonLogAppender : ILogAppender
    {
        /// <inheritdoc/>
        public void Append(string filePath, int lineNumber, string member, LogLevel level, string message, Exception exception = null)
        {   
            var writer = ChooseWriter(level);
            var logMessage = CreateLogMessage(filePath, lineNumber, member, message, LogLevelAsString(level), exception);

            var jsonMessage = logMessage.ToJson();
            writer.WriteLine(jsonMessage);
        }

        static TextWriter ChooseWriter(LogLevel level)
        {
            var writer = Console.Out;
            switch (level)
            {
                case LogLevel.Critical:
                    writer = Console.Error;
                    break;
                case LogLevel.Error:
                    writer = Console.Error;
                    break;
            }
            return writer;

        }

        static string LogLevelAsString(LogLevel level)
        {
            var levelString = string.Empty;
            switch (level)
            {
                case LogLevel.Critical:
                    levelString = "fatal";
                    break;
                case LogLevel.Error:
                    levelString = "error";
                    break;
                case LogLevel.Warning:
                    levelString = "warn";
                    break;
                case LogLevel.Info:
                    levelString = "info";
                    break;
                case LogLevel.Debug:
                    levelString = "debug";
                    break;
                case LogLevel.Trace:
                    levelString = "trace";
                    break;
            }
            return levelString;
        }
        
        JsonLogMessage CreateLogMessage(string filePath, int lineNumber, string member, string message, string logLevel, Exception exception = null)
        {
            return new JsonLogMessage(
                logLevel,
                DateTimeOffset.Now, 
                Guid.NewGuid(), 
                Guid.NewGuid(),
                Guid.NewGuid(), 
                Guid.NewGuid(), 
                filePath, 
                lineNumber, 
                member, 
                message,
                exception?.StackTrace ?? string.Empty
                );
        }
    }
}
