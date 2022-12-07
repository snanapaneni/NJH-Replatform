// Copyright (c) NJH. All rights reserved.

namespace Njh.Kernel.Models
{
    using System;
    using CMS.SiteProvider;

    public sealed class ContextConfig
        : IDisposable
    {
        public string CultureName { get; set; }

        public SiteInfo Site { get; set; }

        /// <summary>
        /// Gets or sets the site name.
        /// </summary>
        public string SiteName { get; set; }

        public bool IsPreview { get; set; }

        public void Dispose()
        {
            // Don't have any unmanaged resources, so do nothing
        }
    }
}
