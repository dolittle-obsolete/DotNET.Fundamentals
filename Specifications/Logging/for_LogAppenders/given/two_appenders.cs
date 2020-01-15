﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace Dolittle.Logging.for_LogAppenders.given
{
    public class two_appenders : all_dependencies
    {
        protected static LogAppenders appenders;
        protected static Mock<ILogAppender> first_appender;
        protected static Mock<ILogAppender> second_appender;

        Establish context = () =>
        {
            appenders = new LogAppenders(log_appenders_configurators.Object);
            first_appender = new Mock<ILogAppender>();
            second_appender = new Mock<ILogAppender>();
            appenders.Add(first_appender.Object);
            appenders.Add(second_appender.Object);
        };
    }
}
