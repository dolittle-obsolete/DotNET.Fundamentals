﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace Dolittle.Logging.for_LogAppenders.given
{
    public class all_dependencies
    {
        protected static Mock<IEnumerable<ICanConfigureLogAppenders>> log_appenders_configurators;
        protected static List<ICanConfigureLogAppenders> actual_log_appenders_configurators;

        Establish context = () =>
        {
            log_appenders_configurators = new Mock<IEnumerable<ICanConfigureLogAppenders>>();
            actual_log_appenders_configurators = new List<ICanConfigureLogAppenders>();
            log_appenders_configurators.Setup(l => l.GetEnumerator()).Returns(() => actual_log_appenders_configurators.GetEnumerator());
        };
    }
}
