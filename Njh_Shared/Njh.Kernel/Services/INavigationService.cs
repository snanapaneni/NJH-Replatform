using CMS.DocumentEngine;
using Njh.Kernel.Models.DTOs;

namespace Njh.Kernel.Services
{
    /// <summary>
    /// Defines the service to work with navigation items.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Get the primary navigation.
        /// </summary>
        /// <returns>list of navigation nodes.</returns>
        IEnumerable<NavItem> GetPrimaryNav();

        /// <summary>
        /// Gets the utility navigation nodes.
        /// </summary>
        /// <returns>list of navigation nodes.</returns>
        IEnumerable<NavItem> GetUtilityNav();

        /// <summary>
        /// Get the CTA item for the specified navigation folder, returns the first encountered.
        /// </summary>
        /// <param name="parentNodeId">node parent id of the CTA.</param>
        /// <returns>The CTA item.</returns>
        CTAItem GetCTAItem(
            int parentNodeId);

        /// <summary>
        /// Get the CTA item for the specified navigation folder, returns the first encountered.
        /// </summary>
        /// <param name="nodeAliasPath">
        /// The alias path to search for the CTA.
        /// </param>
        /// <returns>
        /// The CTA item.
        /// </returns>
        CTAItem GetCTAItem(
            string nodeAliasPath);

        /// <summary>
        /// Gets the main footer navigation, structured by folder.
        /// </summary>
        /// <returns>list of navigation nodes.</returns>
        IEnumerable<NavItem> GetFooterNav();

        /// <summary>
        /// Gets the toe navigation for the bottom of the page.
        /// </summary>
        /// <returns>list of navigation nodes.</returns>
        IEnumerable<NavItem> GetToeNav();

        /// <summary>
        /// Gets all the documents along a node alias path.
        /// </summary>
        /// <param name="path">
        /// The node alias path.
        /// </param>
        /// <returns>
        /// An enumerable of the navigation items.
        /// </returns>
        IEnumerable<NavItem> GetAllItemsInPath(
            string path);

        /// <summary>
        /// Gets all the documents in a section.
        /// </summary>
        /// <param name="currentNode">Takes in a node within that section.</param>
        /// <returns>A strctured tree representing that sub tree.</returns>
        IEnumerable<NavItem> GetSectionNavigation(
            TreeNode currentNode);

        /// <summary>
        /// Public method for setting on path items as active.
        /// </summary>
        /// <param name="currentNode">
        /// TreeNode representing the current page.
        /// </param>
        /// <param name="navItems">
        /// A navigation tree.
        /// </param>
        void SetActiveItems(
            TreeNode currentNode,
            IEnumerable<NavItem> navItems);

        /// <summary>
        /// Set current item as active.
        /// </summary>
        /// <param name="currentNode">
        /// The current page.
        /// </param>
        /// <param name="navItems">
        /// A navigation tree.
        /// </param>
        void SetActiveItem(
            TreeNode currentNode,
            IEnumerable<NavItem> navItems);

        IEnumerable<CTAItem> GetCTAItems(string path);

        IEnumerable<NavItem> GetNavItems<TModel>(string path)
            where TModel : TreeNode, new();
    }
}
