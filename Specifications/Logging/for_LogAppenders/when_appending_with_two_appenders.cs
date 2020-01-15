// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Machine.Specifications;

namespace Dolittle.Logging.for_LogAppenders
{
    public class when_appending_with_two_appenders : given.two_appenders
    {
        const string message = "Some message";
        const string file = "Some file";
        const int line_number = 42;
        const string member = "Some member";
        static Exception exception = new NotImplementedException();

        Because of = () => appenders.Append(file, line_number, member, LogLevel.Error, message, exception);

        It should_forward_to_first_appender = () => first_appender.Verify(a => a.Append(file, line_number, member, LogLevel.Error, message, exception));
        It should_forward_to_second_appender = () => second_appender.Verify(a => a.Append(file, line_number, member, LogLevel.Error, message, exception));
    }
}
