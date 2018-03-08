/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;

namespace Dolittle.Globalization
{
    /// <summary>
    /// Represents a <see cref="ILocalizer"/>
    /// </summary>
    public class Localizer : ILocalizer
    {
        /// <inheritdoc/>
        public LocalizationScope BeginScope()
        {
            var scope = LocalizationScope.FromCurrentThread();

            // TODO: We need to be able to configure culture for the application
            //CultureInfo.CurrentCulture = Configure.Instance.Culture;
            //CultureInfo.CurrentUICulture = Configure.Instance.UICulture;

            return scope;
        }

        /// <inheritdoc/>
        public void EndScope(LocalizationScope scope)
        {
            CultureInfo.CurrentCulture = scope.Culture;
            CultureInfo.CurrentUICulture = scope.UICulture;
        }
    }
}