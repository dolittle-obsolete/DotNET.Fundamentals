// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace Dolittle.Specs.Logging.for_Logger.given
{
    public class all_dependencies
    {
        protected static Mock<ILogAppenders> appenders;

        Establish context = () => appenders = new Mock<ILogAppenders>();
    }
}
