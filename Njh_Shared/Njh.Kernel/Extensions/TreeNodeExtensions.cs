namespace Njh.Kernel.Extensions
{
    using System;
    using Njh.Kernel.Kentico.Models.PageTypes;
    using Njh.Kernel.Services;
    using CMS.DataEngine;
    using CMS.DocumentEngine;
    using CMS.MacroEngine;
    using ReasonOne.Extensions;

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

    }
}
