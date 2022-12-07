// Copyright (c) NJH. All rights reserved.

namespace Njh.Kernel.Services
{
    using System;
    using CMS.DataEngine;
    using CMS.Helpers;

    public class SettingsKeyRepository
        : ISettingsKeyRepository
    {
        public SettingsKeyRepository()
        {
        }

        public T GetValue<T>(
            string keyName,
            string culture = null,
            string siteName = null)
        {
            if (!string.IsNullOrWhiteSpace(culture))
            {
                keyName = $"{keyName}_{CultureHelper.GetShortCultureCode(culture).ToUpper()}";
            }

            var site = siteName; // .ReplaceIfEmpty(_context.Site?.SiteName);

            object value = null;

            var type = typeof(T);

            if (type == typeof(string))
            {
                value = SettingsKeyInfoProvider.GetValue(keyName, site);
            }
            else if (type == typeof(bool))
            {
                value = SettingsKeyInfoProvider.GetBoolValue(keyName, site);
            }
            else if (type == typeof(int))
            {
                value = SettingsKeyInfoProvider.GetIntValue(keyName, site);
            }
            else if (type == typeof(decimal))
            {
                value = SettingsKeyInfoProvider.GetDecimalValue(keyName, site);
            }
            else if (type == typeof(double))
            {
                value = SettingsKeyInfoProvider.GetDoubleValue(keyName, site);
            }
            else if (type == typeof(Guid))
            {
                value = ValidationHelper.GetGuid(
                    SettingsKeyInfoProvider.GetValue(keyName, site),
                    Guid.Empty);
            }

            return value == null
                ? default
                : (T)value;
        }
    }
}