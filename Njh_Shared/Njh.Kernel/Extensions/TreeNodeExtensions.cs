using CMS.DocumentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Extensions
{
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
