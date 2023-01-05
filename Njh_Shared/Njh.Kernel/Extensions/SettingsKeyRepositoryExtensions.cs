using Njh.Kernel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Extensions
{
    /// <summary>
    /// Implements extensions methods for the
    /// <see cref="ISettingsKeyRepository"/> interface.
    /// </summary>
    public static class SettingsKeyRepositoryExtensions
    {


        /// <summary>
        /// Returns the primary navigation page path.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The primary navigation page path.
        /// </returns>
        public static string GetPrimaryNavigationPath(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHPrimaryNavigation");
        }

        /// <summary>
        /// Returns the utility navigation page path.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The utility navigation page path.
        /// </returns>
        public static string GetUtilityNavigationPath(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHUtilityNavigation");
        }

        /// <summary>
        /// Returns the footer navigation page path.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The footer navigation page path.
        /// </returns>
        public static string GetFooterNavigationPath(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHFooterNavigation");
        }

        /// <summary>
        /// Returns the toe navigation page path.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The toe navigation page path.
        /// </returns>
        public static string GetToeNavigationPath(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHToeNavigation");
        }

        public static string GetHeaderLogo(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHHeaderLogo");
        }

        public static string GetHeaderLogoAltText(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHHeaderLogoAltText");
        }

        public static Guid GetMakeAnAppointmentPage(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<Guid>("NJHMakeAnAppointmentPage");
        }

        public static string GetMakeAnAppointmentText(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHMakeAnAppointmentText");
        }

        public static Guid GetGlobalSearchPage(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<Guid>("NJHGlobalSearchPage");
        }

        public static Guid GetGlobalDonatePage(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<Guid>("NJHGlobalDonatePage");
        }

        public static string GetGlobalDonateText(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHGlobalDonateText");
        }

        public static string GetGlobalPhoneNumber(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHGlobalPhoneNumber");
        }

        public static string GetGlobalPhoneNumberText(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHGlobalPhoneNumberText");
        }

        /// <summary>
        /// Gets the page type types.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The page type types.
        /// </returns>
        public static string GetPagePageTypes(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHPagePageTypes");
        }
    }
}
