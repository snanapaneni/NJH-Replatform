namespace Njh.Kernel.Extensions
{
    using System;
    using Njh.Kernel.Kentico.Models.PageTypes;
    using Njh.Kernel.Services;
    using CMS.DataEngine;
    using CMS.DocumentEngine;
    using CMS.Helpers;
    using CMS.MacroEngine;
    using ReasonOne.Extensions;

    /// <summary>
    /// Implements extension methods for the
    /// <see cref="TreeNode"/> class.
    /// </summary>
    public static class TreeNodeExtensions
    {
        /// <summary>
        /// Gets the document type description
        /// to be displayed on the widget.
        /// </summary>
        /// <param name="treeNode">
        /// The document.
        /// </param>
        /// <param name="pageTypeService">
        /// The program network type service.
        /// </param>
        /// <returns>
        /// The page type description.
        /// </returns>
        public static string GetPageTypeName(
            this TreeNode treeNode,
            IPageTypeService pageTypeService)
        {
            return
                pageTypeService != null
                    ? pageTypeService.GetPageTypeName(treeNode)
                    : string.Empty;
        }

        /// <summary>
        /// Gets the routing URL of the document
        /// to be displayed on the widget.
        /// </summary>
        /// <param name="document">
        /// The document.
        /// </param>
        /// <returns>
        /// The URL of the document or
        /// the link URL to the document,
        /// if it is a link page type.
        /// </returns>
        public static string GetRoutingUrl(
            this TreeNode document)
        {
            //var url =
            //    document is PageType_LinkPage
            //        ? document.GetStringValue(
            //            nameof(PageType_LinkPage.URL),
            //            DocumentURLProvider.GetUrl(document))
            //        : DocumentURLProvider.GetUrl(document);

            //return url;
            throw new NotImplementedException();
        }

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
        /// Returns the absolute image URL for the Open Graph image tag.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <returns>
        /// The absolute image URL. Empty string, otherwise.
        /// </returns>
        public static string GetOpenGraphImage(
            this TreeNode page)
        {
            var mainImage =
                ValidationHelper.GetString(
                    page.GetValue("MainImage"),
                    string.Empty);

            return
                page != null &&
                page.Site != null &&
                !string.IsNullOrWhiteSpace(mainImage)
                    ? DocumentURLProvider.GetAbsoluteUrl(
                        mainImage,
                        new SiteInfoIdentifier(page.Site.SiteID))
                    : string.Empty;
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
        public static string GetTwitterCardImage(
            this TreeNode page)
        {
            var thumbnailImage =
                ValidationHelper.GetString(
                    page.GetValue("ThumbnailImage"),
                    string.Empty);

            var mainImage =
                ValidationHelper.GetString(
                    page.GetValue("MainImage"),
                    string.Empty);

            var winnerImage =
                string.IsNullOrWhiteSpace(thumbnailImage)
                    ? mainImage
                    : thumbnailImage;

            var absoluteImageUrl =
                page != null &&
                page.Site != null &&
                !string.IsNullOrWhiteSpace(winnerImage)
                    ? DocumentURLProvider.GetAbsoluteUrl(
                        winnerImage,
                        new SiteInfoIdentifier(page.Site.SiteID))
                    : string.Empty;

            return absoluteImageUrl;
        }

        /// <summary>
        /// Returns the image URL for the Twitter Card image tag.
        /// </summary>
        /// <param name="imageUrl">
        /// The image URL.
        /// </param>
        /// <returns>
        /// The image URL. Empty string, otherwise.
        /// </returns>
        private static string ResizeImageForTwitter(
            string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return string.Empty;
            }

            var uriBuilder = new UriBuilder(imageUrl);

            uriBuilder.UpsertQueryString("width", "432");

            return uriBuilder.ToString();
        }
    }
}
