namespace Njh.Kernel.Extensions
{
    using System;
    using Njh.Kernel.Kentico.Models.PageTypes;
    using Njh.Kernel.Services;
    using CMS.DataEngine;
    using CMS.DocumentEngine;
    using CMS.MacroEngine;
    using ReasonOne.Extensions;
    using CMS.Helpers;

    /// <summary>
    /// Implements extension methods for the
    /// <see cref="TreeNode"/> class.
    /// </summary>
    public static class TreeNodeExtensions
    {
        /// <summary>
        /// Converts a tree node to a specific page type.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the page type.
        /// </typeparam>
        /// <param name="page">
        /// The tree node.
        /// </param>
        /// <returns>
        /// The object with the specific page type.
        /// null, otherwise.
        /// </returns>
        public static T ToPageType<T>(
            this TreeNode page)
            where T : TreeNode, new()
        {
            if (page == null)
            {
                return null;
            }

            if (typeof(T) == page.GetType())
            {
                return (T)page;
            }

            var ds = page.GetDataSet();

            if ((ds == null) || (ds.Tables.Count < 1))
            {
                return null;
            }

            var dt = ds.Tables[0];

            if (dt.Rows.Count < 1)
            {
                return null;
            }

            return TreeNode.New<T>(
                page.NodeClassName,
                dt.Rows[0]);
        }


        /// <summary>
        /// Get meta tag of the page.
        /// The Kentico ones are not working properly.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="metaTagName">
        /// The meta tag name.
        /// </param>
        /// <returns>
        /// The meta tag.
        /// </returns>
        public static string GetMetaTag(
            this TreeNode page,
            string metaTagName)
        {
            var resolver = MacroContext.CurrentResolver;

            resolver.SetAnonymousSourceData(page);

            var data = page.GetStringValue(metaTagName, string.Empty);

            if (string.IsNullOrWhiteSpace(data))
            {
                data = page.GetInheritedValue(metaTagName)?.ToString();
            }

            return resolver.ResolveMacros(data);
        }

        /// <summary>
        /// Returns the absolute image URL for the Twitter image tag.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <returns>
        /// The absolute image URL. Empty string, otherwise.
        /// </returns>
        public static string GetSocialImage(
            this TreeNode page)
        {
            var pageImage =
                ValidationHelper.GetString(
                    page.GetValue(nameof(IPageType_BasePageType.PageImage)),
                    string.Empty);

            var absoluteImageUrl =
                page != null &&
                page.Site != null &&
                !string.IsNullOrWhiteSpace(pageImage)
                    ? DocumentURLProvider.GetAbsoluteUrl(
                        pageImage,
                        new SiteInfoIdentifier(page.Site.SiteID))
                    : string.Empty;

            return absoluteImageUrl;
        }
    }
}
