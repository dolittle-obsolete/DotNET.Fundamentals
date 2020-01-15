// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Machine.Specifications;

namespace Dolittle.Logging.for_LogAppenders
{
    public class when_clearing_log_appenders : given.one_appender
    {
        Because of = () => appenders.Clear();

        It should_not_have_any_appenders = () => appenders.Appenders.ShouldBeEmpty();
    }
}
