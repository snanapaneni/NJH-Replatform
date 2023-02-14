// Copyright (c) Njh. All rights reserved.

using System.Text.RegularExpressions;

namespace Njh.Kernel.Definitions
{
    /// <summary>
    /// Defines site-specific constants.
    /// </summary>
    public static class GlobalConstants
    {
        
        /// <summary>
        /// Defines the site code name.
        /// </summary>
        public const string SiteCodeName = "NJH";

       
        /// <summary>
        /// Defines custom table constants.
        /// </summary>
        public static class CustomTables
        {
            /// <summary>
            /// Defines the page and template custom table name.
            /// </summary>
            public const string PageAndTemplate = "RO.CustomTable_PageAndTemplate";
        }

        /// <summary>
        /// Defines caching constants.
        /// </summary>
        public static class Caching
        {
            // TODO: Fix so that its pulled from site settings

            /// <summary>
            /// Defines a value indicating whether the caching is enabled.
            /// </summary>
            public const bool CacheEnabled = true;

            /// <summary>
            /// Defines the cache expiry time in minutes.
            /// </summary>
            public const int CacheExpiryMinutes = 10;

            /// <summary>
            /// Defines the cache expiry time in minutes.
            /// </summary>
            public const int CacheExpiryMinutesShort = 5;
        }

        public static class Regexs
        {
            public static Regex RxPhone = new Regex(@"(\+\d{1})?(\d{3})(\d{3})(\d{4})");
            public const string PhoneDisplayFormat = "$2.$3.$4";
        }

        /// <summary>
        /// Miscellaneous values for use in utility and component classes.
        /// </summary>
        public static class Utils
        {
            /// <summary>
            /// Days of the week as strings in Title Case, from Monday to Sunday.
            /// </summary>
            public static string[] DaysOfTheWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        }
    }
}