﻿// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Logging;
using Machine.Specifications;

namespace Dolittle.Specs.Logging.for_Logger.given
{
    public class a_logger_and_reusable_details : all_dependencies
    {
        protected const string message = "Some message";
        protected const string file = "Some file";
        protected const int line_number = 42;
        protected const string member = "Some member";
        protected static Exception exception = new NotImplementedException();

        protected static Logger logger;

        Establish context = () => logger = new Logger(appenders.Object);
    }
}
