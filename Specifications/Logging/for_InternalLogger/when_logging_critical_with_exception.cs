// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Logging;
using Machine.Specifications;

namespace Dolittle.Specs.Logging.for_InternalLogger
{
    public class when_logging_critical_with_exception : given.a_logger_with_two_writers
    {
        static string message;
        static object[] arguments;
        static Exception exception;

        Establish context = () =>
        {
            message = "Some message";
            arguments = new object[] { "some arguments", 1 };
            exception = new Exception("Some exception");
        };

        Because of = () => logger.Critical(exception, message, arguments);

        It should_forward_to_writer_one_with_level_critical_with_exception = () =>
        {
            writer_one.Verify(_ => _.Write(LogLevel.Critical, exception, message, arguments), Moq.Times.Once());
            writer_one.VerifyNoOtherCalls();
        };

        It should_forward_to_writer_two_with_level_critical_with_exception = () =>
        {
            writer_two.Verify(_ => _.Write(LogLevel.Critical, exception, message, arguments), Moq.Times.Once());
            writer_two.VerifyNoOtherCalls();
        };
    }
}