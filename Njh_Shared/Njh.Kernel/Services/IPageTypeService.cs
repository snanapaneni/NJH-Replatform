namespace Njh.Kernel.Services
{
    using System;
    using CMS.DocumentEngine;

    /// <summary>
    /// Defines the service to work with page types.
    /// </summary>
    public interface IPageTypeService
    {
        /// <summary>
        /// Returns the name of the page type by node GUID.
        /// </summary>
        /// <param name="nodeGuid">
        /// The node GUID of the document.
        /// </param>
        /// <returns>
        /// The name of the page type.
        /// </returns>
        string GetPageTypeNameByGuid(Guid nodeGuid);

        /// <summary>
        /// Returns the name of the page type.
        /// </summary>
        /// <param name="treeNode">
        /// The document.
        /// </param>
        /// <returns>
        /// The name of the page type.
        /// </returns>
        string GetPageTypeName(TreeNode treeNode);

        /// <summary>
        /// Returns the name of the page type.
        /// </summary>
        /// <param name="className">
        /// The class name.
        /// </param>
        /// <param name="pageTypeName">
        /// The document.
        /// </param>
        /// <returns>
        /// The name of the page type.
        /// </returns>
        string GetPageTypeName(
            string className,
            string pageTypeName);
    }
}
