using Njh.Kernel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Extensions
{
    public static class PressReleaseSettingsExtensions
    {
        public static string GetMediaResources(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHMediaResources");
        }

        public static string GetMediaContacts(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHMediaContacts");
        }
    }
}
