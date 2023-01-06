namespace Njh.Kernel.Services
{
    using System;
    using System.Linq;
    using CMS.DocumentEngine;
    using CMS.Helpers;
    using Njh.Kernel.Models;
    using Njh.Kernel.Definitions;

    public class CacheService
         : ServiceBase, ICacheService
    {
        private readonly AppCacheConfig cacheConfig;

        private readonly ContextConfig context;

        public CacheService(
            ContextConfig context)
            : this(
                new AppCacheConfig
                {
                    Enabled = GlobalConstants.Caching.CacheEnabled,
                    CacheExpiryMinutes = GlobalConstants.Caching.CacheExpiryMinutes,
                },
                context)
        {
        }

        public CacheService(
            AppCacheConfig config,
            ContextConfig context)
        {
            this.cacheConfig = config;
            this.context = context;
        }

        public T Get<T>(
            Func<T> func,
            CacheParameters settings)
        {
            return this.Get(
                cp => func(),
                settings);
        }

        public T Get<T>(
            Func<CacheParameters, T> func,
            CacheParameters parameters)
        {
            CacheSettings cacheSettings;

            if (!this.TryGetCacheSettings(parameters, out cacheSettings))
            {
                return func(parameters);
            }

            return this.Get(func, parameters, cacheSettings);
        }

        public T Get<T>(
            Func<T> func,
            string cacheKey,
            bool isCultureSpecific = true,
            bool isSiteSpecific = true,
            int? duration = null,
            params string[] cacheDependencies)
        {
            return this.Get(
                cp => func(),
                cacheKey,
                isCultureSpecific,
                isSiteSpecific,
                duration,
                cacheDependencies);
        }

        public T Get<T>(
            Func<CacheParameters, T> func,
            string cacheKey,
            bool isCultureSpecific = true,
            bool isSiteSpecific = true,
            int? duration = null,
            params string[] cacheDependencies)
        {
            return this.Get(
                func,
                new CacheParameters
                {
                    CacheKey = cacheKey,
                    IsCultureSpecific = isCultureSpecific,
                    IsSiteSpecific = isSiteSpecific,
                    Duration = duration,
                    CacheDependencies = cacheDependencies.ToList(),
                });
        }

        public string GetCacheKey(
            string baseKey,
            string cultureName = null,
            string siteName = null)
        {
            return CacheHelper.BuildCacheItemName(
                new[]
                {
                    baseKey,
                    cultureName,
                    siteName,
                }
                    .Where(p => !string.IsNullOrWhiteSpace(p)));
        }

        /// <inheritdoc />
        public void TouchCacheKeys(
            params string[] cacheKeys)
        {
            CacheHelper.TouchKeys(
                cacheKeys,
                logTasks: true,
                ensureKeys: false);
        }

        public void TouchCacheKeys(
            TreeNode page)
        {
            // This will touch all of the page-related cache keys
            // (see https://docs.kentico.com/k11/configuring-kentico/optimizing-website-performance/configuring-caching/setting-cache-dependencies
            // for the full reference)
            page?.ClearCache();
        }

        public bool TryGet<T>(
            string cacheKey,
            out T obj)
        {
            return CacheHelper.TryGetItem(
                cacheKey,
                out obj);
        }

        public bool TrySet<T>(
            T obj,
            CacheParameters parameters)
        {
            if (!this.TryGetCacheSettings(parameters, out CacheSettings cacheSettings))
            {
                return false;
            }

            // Forcefully remove the cached item, so that it is replaced by the new value
            // (N.B. Have to do it this way, because we are using dynamic caching by passing a function instead of setting a value,
            // and Kentico is going to wait for the cache expiration otherwise)
            CacheHelper.Remove(cacheSettings.CacheItemName);

            this.Get(cp => obj, parameters, cacheSettings);

            return true;
        }

        protected T Get<T>(
            Func<CacheParameters, T> func,
            CacheParameters parameters,
            CacheSettings cacheSettings)
        {
            var cacheKey = cacheSettings.CacheItemName;

            object preliminaryResult;

            // NOTE: If a cache is declared as a cache dependency, but doesn't yet have a cached value,
            // Kentico automatically sets its cached value to an instance of DummyItem. This means that
            // if we wanted to dynamically cache a value against that key later, our dynamic function
            // would not actually be evaluated, since Kentico would consider the dummy object as
            // an existing cached value. That is why we are explicitly removing the dummy object here,
            // so that it does not interfere with out caching mechanism.
            if (this.TryGet(cacheKey, out preliminaryResult)
                && (preliminaryResult is DummyItem))
            {
                CacheHelper.Remove(cacheKey);
            }

            var cachedResult = CacheHelper.Cache(
                cs =>
                {
                    var result = func(parameters);

                    var dependencies = parameters.CacheDependencies;

                    // Update cache settings with the dependencies (both initial and dynamically obtained)
                    if ((dependencies != null) && (dependencies.Count > 0))
                    {
                        // Remove duplicates
                        dependencies = dependencies.Distinct().ToList();

                        cacheSettings.CacheDependency =
                            CacheHelper.GetCacheDependency(dependencies);
                    }

                    // Return the actual data to be cached
                    return result;
                },
                cacheSettings);

            // If null value is not allowed to be cached, dump the cache
            if (!parameters.AllowNullValue && (cachedResult == null))
            {
                CacheHelper.Remove(cacheKey);
            }

            return cachedResult;
        }

        protected bool TryGetCacheSettings(
            CacheParameters parameters,
            out CacheSettings cacheSettings)
        {
            cacheSettings = null;

            if (!this.cacheConfig.Enabled)
            {
                return false;
            }

            if (parameters == null)
            {
                return false;
            }

            // If the site name is not provided, use current site name
            if (string.IsNullOrEmpty(parameters.SiteName))
            {
                parameters.SiteName = this.context.Site?.SiteName;
            }

            // If the culture code is not provided, use current culture code
            if (string.IsNullOrEmpty(parameters.CultureCode))
            {
                parameters.CultureCode = this.context.CultureName;
            }

            // If a specific value is provided, it has priority
            var minutes = parameters.Duration ?? this.cacheConfig.CacheExpiryMinutes;

            var cacheNameParts = new[]
            {
                parameters.CacheKey,
                parameters.IsCultureSpecific
                    ? parameters.CultureCode
                    : null,
                parameters.IsSiteSpecific
                    ? parameters.SiteName
                    : null,
            };

            cacheSettings =
                new CacheSettings(
                    minutes,
                    parameters.IsSlidingExpiration,
                    cacheNameParts
                        .Where(p => !string.IsNullOrEmpty(p))
                        .ToArray<object>());

            cacheSettings.CacheItemPriority = CMSCacheItemPriority.High;

            return true;
        }
    }
}
