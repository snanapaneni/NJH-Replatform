// Copyright (c) Njh. All rights reserved.

namespace Njh.Kernel.Models
{
    public class AppCacheConfig
        : IConfig
    {
        public bool Enabled { get; set; }

        public int CacheExpiryMinutes { get; set; }
    }
}