namespace Njh.Kernel.Extensions
{
    using Njh.Kernel.Services;

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

        public static string GetMakeAnAppointmentPage(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHMakeAnAppointmentPage");
        }

        public static string GetMakeAnAppointmentText(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHMakeAnAppointmentText");
        }

        public static string GetGlobalSearchPage(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHGlobalSearchPage");
        }

        public static string GetGlobalDonatePage(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHGlobalDonatePage");
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


        /// <summary>
        /// Gets the analytics script (i.e. GTM).
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The analytics script (GTM).
        /// </returns>
        public static string GetAnalyticsScript(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHAnalyticsScript");
        }

        public static string GetPolicyLinksPath(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHPolicyLinksPath");
        }

        public static string GetBadgesPath(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHBadgesPath");
        }

        public static string GetAddress(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHAddress");
        }

        public static string GetAddressUrl(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHAddressUrl");
        }

        public static string GetSupportedLanguagesPath(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHSupportedLanguagesPath");
        }

        public static string GetFooterSocialMediaLinksPath(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHFooterSocialMediaLinksPath");
        }

        public static string GetFooterButtonsPath(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHFooterButtonsPath");
        }

        public static string GetAlertPath(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHAlertPath");
        }
        
        public static string GetPageTitlePrefix(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("CMSPageTitlePrefix");
        }

        public static string GetPageTitleFormat(
           this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("CMSPageTitleFormat");
        }
    }
}
