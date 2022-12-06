// Copyright (c) Njh. All rights reserved.

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
    }
}