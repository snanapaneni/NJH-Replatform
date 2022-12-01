// Copyright (c) Toronto Region Board of Trade. All rights reserved.

namespace Bot.Mvc.Controllers
{
    using System;
    using Kentico.Content.Web.Mvc;
    using Microsoft.AspNetCore.Mvc;

    [ApiExplorerSettings(IgnoreApi = true)]
    public class BaseController
        : Controller
    {
        private readonly IPageUrlRetriever pageUrlRetriever;

        public BaseController(
            IPageUrlRetriever pageUrlRetriever)
        {
            this.pageUrlRetriever = pageUrlRetriever ??
                throw new ArgumentNullException(nameof(pageUrlRetriever));
        }

        /// <summary>
        /// Redirects to a local URL.
        /// </summary>
        /// <param name="returnUrl">Local URL to redirect to.</param>
        /// <returns>Redirect to a URL.</returns>
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) &&
                this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToLocal(this.GetHomeUrl());
        }

        /// <summary>
        /// Gets the home page URL.
        /// </summary>
        /// <returns>Home page URL.</returns>
        protected string GetHomeUrl() =>
            this.pageUrlRetriever.Retrieve("/Home").RelativePath;
    }
}