// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Logging;
using Machine.Specifications;

namespace Dolittle.Specs.Logging.for_InternalLogger
{
    public class when_a_writer_throws_an_exception : given.a_logger_with_one_failing_and_one_successful_writer
    {
        static string message;
        static object[] arguments;
        static Exception exception;

        Establish context = () =>
        {
            message = "Some message";
            arguments = new object[] { "some arguments", 1 };
        };

        Because of = () => exception = Catch.Exception(() => logger.Information(message, arguments));

        It should_forward_to_writer_one = () => writer_one.Verify(_ => _.Write(LogLevel.Info, message, arguments), Moq.Times.Once());
        It should_forward_to_writer_two = () => writer_two.Verify(_ => _.Write(LogLevel.Info, message, arguments), Moq.Times.Once());
        It should_not_fail = () => exception.ShouldBeNull();
    }
}