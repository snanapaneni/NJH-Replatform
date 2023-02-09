namespace Njh.Kernel.Extensions
{
    using Njh.Kernel.Services;

    /// <summary>
    /// Implements extensions methods for the
    /// <see cref="ISettingsKeyRepository"/> interface.
    /// </summary>
    public static class PressGaneySettingsExtensions
    {
        /// <summary>
        /// Returns the Press Ganey Application Id.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The Press Ganey Application Id.
        /// </returns>
        public static string GetPressGaneyApplicationId(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHPgApplicationId");
        }

        /// <summary>
        /// Returns the Press Ganey Application Secret.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The Press Ganey Application Secret.
        /// </returns>
        public static string GetPressGaneyApplicationSecret(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHPgApplicationSecret");
        }

        /// <summary>
        /// Returns the Press Ganey Base Url.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The Press Ganey Base Url.
        /// </returns>
        public static string GetPressGaneyBaseUrl(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHPgBaseUrl");
        }

        /// <summary>
        /// Returns the Press Ganey Physicians Path.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The Press Ganey Physicians Path.
        /// </returns>
        public static string GetPressGaneyPhysiciansPath(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHPgPhysiciansPath");
        }

        /// <summary>
        /// Returns the PressGaney Page Types.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The Press Ganey Page Types.
        /// </returns>
        public static string GetPressGaneyPageTypes(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHPgPageTypes");
        }

        /// <summary>
        /// Returns the Press Ganey Min Rating Count.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The Press Ganey Min Rating Count.
        /// </returns>
        public static int GetPressGaneyMinRatingCount(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<int>("NJHPgMinRatingCount");
        }

        /// <summary>
        /// Returns the Press Ganey Cache Duration in Min.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The Press Ganey Cache Duration in Min.
        /// </returns>
        public static int GetPressGaneyCacheDuration(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<int>("NJHPgCacheDuration");
        }

        /// <summary>
        /// Returns the Press Ganey Comments And Ratings Url Path.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The Press Ganey Comments And Ratings Url Path.
        /// </returns>
        public static string GetPressGaneyCommentsAndRatingsURLPath(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHPgCommentsAndRatingsUrlPath");
        }

        /// <summary>
        /// Returns the Press Ganey Comments And Ratings Url Path.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The Press Ganey Comments And Ratings Url Path.
        /// </returns>
        public static string GetPressGaneyCommentFilter(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHPgCommentFilter");
        }

        /// <summary>
        /// Returns the Press Ganey Authentication Url.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The Press Ganey Authentication Url.
        /// </returns>
        public static string GetPressGaneyAuthenticationUrl(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHPgAuthenticationUrl");
        }

        /// <summary>
        /// Returns the Press Ganey Authentication Url.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <returns>
        /// The Press Ganey Authentication Url.
        /// </returns>
        public static string GetPressGaneyServerTimeZoneString(
            this ISettingsKeyRepository settingsKeyRepository)
        {
            return settingsKeyRepository
                .GetValue<string>("NJHPgServerTimeZoneString");
        }

    }
}