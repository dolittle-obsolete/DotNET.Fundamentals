// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Logging.for_LogAppenders
{
    public class when_adding_log_appender : given.no_log_appenders
    {
        static ILogAppender appender;

        Establish context = () => appender = Mock.Of<ILogAppender>();

        Because of = () => appenders.Add(appender);

        It should_contain_the_appender = () => appenders.Appenders.ShouldContainOnly(appender);
    }
}
