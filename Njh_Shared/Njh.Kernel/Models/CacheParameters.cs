﻿// Copyright (c) Njh. All rights reserved.

namespace Njh.Kernel.Models
{
    using System.Collections.Generic;

    public class CacheParameters
    {
        private List<string> cacheDependencies;

        public bool AllowNullValue { get; set; } = false;

        public string CacheKey { get; set; }

        public string CultureCode { get; set; }

        public string SiteName { get; set; }

        public int? Duration { get; set; }

        public bool IsCultureSpecific { get; set; } = true;

        public bool IsSiteSpecific { get; set; } = true;

        public bool IsSlidingExpiration { get; set; } = false;

        public List<string> CacheDependencies
        {
            get
            {
                return this.cacheDependencies ?? (this.cacheDependencies = new List<string>());
            }

            set
            {
                this.cacheDependencies = value;
            }
        }
    }
}
