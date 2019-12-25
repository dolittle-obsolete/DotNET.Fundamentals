// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace Dolittle.Logging.for_LogAppenders.given
{
    public class one_appender : all_dependencies
    {
        protected static LogAppenders appenders;
        protected static Mock<ILogAppender> appender;

        Establish context = () =>
        {
            appenders = new LogAppenders(log_appenders_configurators.Object);
            appender = new Mock<ILogAppender>();
            appenders.Add(appender.Object);
        };
    }
}
