﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Microsoft.Extensions.Localization
{
    /// <summary>
    /// An <see cref="IStringLocalizer"/> that uses the <see cref="System.Resources.ResourceManager"/> and
    /// <see cref="System.Resources.ResourceReader"/> to provide localized strings for a specific <see cref="CultureInfo"/>.
    /// </summary>
    public class GlobalResourceManagerWithCultureStringLocalizer : GlobalResourceManagerStringLocalizer
    {
        private readonly CultureInfo _culture;

        /// <summary>
        /// Creates a new <see cref="ResourceManagerWithCultureStringLocalizer"/>.
        /// </summary>
        /// <param name="resourceManager">The <see cref="System.Resources.ResourceManager"/> to read strings from.</param>
        /// <param name="resourceAssembly">The <see cref="Assembly"/> that contains the strings as embedded resources.</param>
        /// <param name="baseName">The base name of the embedded resource in the <see cref="Assembly"/> that contains the strings.</param>
        /// <param name="resourceNamesCache">Cache of the list of strings for a given resource assembly name.</param>
        /// <param name="culture">The specific <see cref="CultureInfo"/> to use.</param>
        /// <param name="globalResourceOptions">The <see cref="GlobalResourceOptions"/></param>
        /// <param name="globalResourceManager">The global <see cref="System.Resources.ResourceManager"/> to read strings from.</param>
        public GlobalResourceManagerWithCultureStringLocalizer(
            ResourceManager resourceManager,
            Assembly resourceAssembly,
            string baseName,
            IResourceNamesCache resourceNamesCache,
            CultureInfo culture,
            GlobalResourceOptions globalResourceOptions,
            ResourceManager globalResourceManager = null
            )
            : base(
                  resourceManager, 
                  resourceAssembly, 
                  baseName, 
                  resourceNamesCache,
                  globalResourceOptions,
                  globalResourceManager
                  )
        {
            if (resourceManager == null)
            {
                throw new ArgumentNullException(nameof(resourceManager));
            }

            if (resourceAssembly == null)
            {
                throw new ArgumentNullException(nameof(resourceAssembly));
            }

            if (baseName == null)
            {
                throw new ArgumentNullException(nameof(baseName));
            }

            if (resourceNamesCache == null)
            {
                throw new ArgumentNullException(nameof(resourceNamesCache));
            }

            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            _culture = culture;
        }

        /// <inheritdoc />
        public override LocalizedString this[string name]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var value = GetStringSafely(name, _culture);
                return new LocalizedString(name, value ?? name);
            }
        }

        /// <inheritdoc />
        public override LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var format = GetStringSafely(name, _culture);
                var value = string.Format(_culture, format ?? name, arguments);
                return new LocalizedString(name, value ?? name, resourceNotFound: format == null);
            }
        }

        /// <inheritdoc />
        public override IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) =>
            GetAllStrings(includeParentCultures, _culture);
    }
}
