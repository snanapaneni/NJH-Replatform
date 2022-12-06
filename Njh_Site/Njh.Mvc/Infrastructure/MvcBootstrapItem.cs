// Copyright (c) NJH. All rights reserved.

namespace Njh.Mvc.Infrastructure
{
    using Autofac;
    using CMS.Localization;
    using CMS.SiteProvider;
    using Njh.Kernel.Definitions;
    using Njh.Kernel.Models;
    using Njh.Kernel.Repositories;
    using Njh.Kernel.Services;
    using System;
    using System.Reflection;
    using ReasonOne.KenticoXperience.DependencyInjection;

    public class MvcBootstrapItem
        : IBootstrapItem
    {
        public void ConfigureDependencies(
            ContainerBuilder builder,
            params Assembly[] assemblies)
        {
            RegisterServices(builder, assemblies);

            RegisterRepositories(builder, assemblies);
        }

        /// <summary>
        /// Note the service name must end with service and must implement the IService interface
        /// to be automatically injected. This is VERY important.
        /// The name of the services must end in Service.
        /// </summary>
        private static void RegisterServices(
            ContainerBuilder builder,
            params Assembly[] assemblies)
        {
            builder
                .Register(ctx => GetCurrentContext());

            // Uncomment below to exclude certain services to register this way
            // ,new[]
            // {
            //    typeof(IEmailService),
            //    typeof(IMvcEmailService),
            //    typeof(IOnePlaceDataService),
            //    typeof(IPasswordPolicyService),
            //    typeof(IIpLocatorService)
            // }
            builder
                .RegisterImplementations<IService>(
                    assemblies,
                    "Service")

                // Note that we have to use anonymous functions/context,
                // so that context values are evaluated at the time of injection
                .WithParameter(
                    (parameter, context) =>
                        parameter.ParameterType == typeof(ContextConfig),
                    (parameter, context) =>
                        GetCurrentContext());
        }

        private static ContextConfig GetCurrentContext()
        {
            return new ContextConfig
            {
                CultureName = LocalizationContext.PreferredCultureCode,
                Site = GetCurrentSite(),
                SiteName = GetCurrentSite().SiteName,
                IsPreview = IsPreviewMode(),
            };
        }

        private static bool IsPreviewMode()
        {
            return false;
        }

        private static SiteInfo GetCurrentSite()
        {
            SiteInfo currentSite = null;

            // Kentico tries to access the request context when determining the current site.
            // There doesn't seem to be a more elegant way of checking if the request context has been defined,
            // which is why we are catching the "Request is not available in this context" error instead.
            try
            {
                currentSite = SiteContext.CurrentSite;
            }
            catch (Exception)
            {
                // Do nothing
            }

            return currentSite ?? new SiteInfo
            {
                // Imitate the site
                SiteName = GlobalConstants.SiteCodeName,
            };
        }

        /// <summary>
        /// Note the Repository name must end with Repository and must implement the IRepository interface
        /// to be automatically injected.
        /// </summary>
        private static void RegisterRepositories(
            ContainerBuilder builder,
            params Assembly[] assemblies)
        {
            builder
                .RegisterImplementations<IRepository>(
                    assemblies,
                    "Repository")

                // Note that we have to use anonymous functions/context,
                // so that context values are evaluated at the time of injection
                .WithParameter(
                    (parameter, context) =>
                        parameter.ParameterType == typeof(ContextConfig),
                    (parameter, context) =>
                        GetCurrentContext());
        }
    }
}
