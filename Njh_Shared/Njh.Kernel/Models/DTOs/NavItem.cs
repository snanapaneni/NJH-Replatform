
#nullable enable
using CMS.SalesForce;

namespace Njh.Kernel.Models.DTOs
{
    using System;
    using System.Collections.Generic;
    using CMS.Ecommerce;
    using Mapster;

    /// <summary>
    /// Represents a navigation item.
    /// </summary>
    public class NavItem
        : LinkItem
    {
        /// <summary>
        /// Gets or sets the link page.
        /// </summary>
        public Guid LinkPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the item is clickable.
        /// </summary>
        /// <remarks>
        /// Adding this field because some nav items will be used for display
        /// Headings Only.
        /// </remarks>
        public bool IsClickable { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating
        /// whether the item is on the path.
        /// </summary>
        public bool IsOnPath { get; set; } = false;

        /// <summary>
        /// Gets or sets the child nav items.
        /// </summary>
        [AdaptIgnore]
        public List<NavItem> Children { get; set; } =
            new List<NavItem>();

        /// <summary>
        /// Gets or sets the CTA item.
        /// </summary>
        public CTAItem? CTAItem { get; set; } = null;

        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Checks if there is
        /// any child nav items on the path.
        /// </summary>
        /// <returns>
        /// true, if there is any child on the path.
        /// false, otherwise.
        /// </returns>
        public bool IsChildOnPath()
        {
            return this.IsChildOnPath(this);
        }

        /// <summary>
        /// Checks if there is
        /// any child nav items on the path.
        /// </summary>
        /// <returns>
        /// true, if there is any child on the path.
        /// false, otherwise.
        /// </returns>
        private bool IsChildOnPath(
            NavItem navItem)
        {
            var isChildOnPath = false;

            foreach (var childNavItem in navItem.Children)
            {
                if (childNavItem.IsOnPath)
                {
                    isChildOnPath = childNavItem.IsOnPath;
                    break;
                }

                isChildOnPath =
                    this.IsChildOnPath(childNavItem);
            }

            return isChildOnPath;
        }
    }
}
#nullable restore