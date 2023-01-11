﻿using CMS.DocumentEngine;
using Njh.Kernel.Kentico.Models.PageTypes;
using Njh.Kernel.Models.DTOs;
using Njh.Kernel.Models;
using Mapster;
using Njh.Kernel.Constants;
using Njh.Kernel.Extensions;

namespace Njh.Kernel.Services
{
    /// <summary>
    /// A service for handling navigation related components.
    /// </summary>
    public class NavigationService
        : ServiceBase, INavigationService
    {
        private readonly ISettingsKeyRepository settingsKeyRepository;
        private readonly ICacheService cacheService;
        private readonly ContextConfig context;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NavigationService"/> class.
        /// </summary>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <param name="cacheService">
        /// The cache service.
        /// </param>
        /// <param name="context">
        /// The context config.
        /// </param>
        public NavigationService(
            ISettingsKeyRepository settingsKeyRepository,
            ICacheService cacheService,
            ContextConfig context)
        {
            this.settingsKeyRepository = settingsKeyRepository ??
                throw new ArgumentNullException(nameof(settingsKeyRepository));

            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));

            this.context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public IEnumerable<NavItem> GetPrimaryNav()
        {
            var navItems = this.GetNavItemsByPath(this.settingsKeyRepository.GetPrimaryNavigationPath());
            var headerNav = StructureChildren(navItems);

            foreach (var navItem in headerNav)
            {
                navItem.CTAItem = this.GetCTAItem(navItem.NodeID);
            }

            return headerNav;
        }

        /// <inheritdoc/>
        public IEnumerable<NavItem> GetUtilityNav()
        {
            var navItems = this.GetNavItemsByPath(this.settingsKeyRepository.GetUtilityNavigationPath()).Take(4);
            return navItems;
        }

        /// <inheritdoc/>
        public IEnumerable<NavItem> GetFooterNav()
        {
            var navItems = this.GetNavItemsByPath(this.settingsKeyRepository.GetFooterNavigationPath());
            var footerNav = StructureChildren(navItems);

            return footerNav;
        }

        /// <inheritdoc/>
        public IEnumerable<NavItem> GetToeNav()
        {
            var navItems = this.GetNavItemsByPath(this.settingsKeyRepository.GetToeNavigationPath());
            return navItems;
        }

        /// <inheritdoc/>
        public CTAItem GetCTAItem(int nodeId)
        {
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByIdByType,
                    "NavigationCTA",
                    nodeId,
                    PageType_CTAItem.CLASS_NAME),
                IsCultureSpecific = true,
                CultureCode = this.context?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.context?.Site?.SiteName,
            };
            var result = this.cacheService.Get(
                cp =>
                {
                    // Find the first cta item under the specified node
                    var ctaItem = DocumentHelper.GetDocuments<PageType_CTAItem>()
                        .OnCurrentSite()
                        .CombineWithDefaultCulture()
                        .WhereEquals("NodeParentID", nodeId)
                        .Published()
                        .OrderBy("NodeOrder")
                        .TopN(1)
                        .ProjectToType<CTAItem>()
                        .FirstOrDefault();

                    // If there is a CTA item get it's url
                    if (ctaItem != null)
                    {
                        var linkedPage = DocumentHelper.GetDocuments()
                            .OnCurrentSite()
                            .CombineWithDefaultCulture()
                            .LatestVersion()
                            .WhereEquals("NodeGuid", ctaItem.LinkPage)
                            .OrderBy("NodeOrder")
                            .TopN(1)
                            .FirstOrDefault();

                        if (linkedPage != null)
                        {
                            ctaItem.Link = DocumentURLProvider.GetAbsoluteUrl(linkedPage);
                        }
                    }

                    cp.CacheDependencies.Add(
                        this.cacheService.GetCacheKey(
                            string.Format(DummyCacheKeys.PageNodeId, nodeId),
                            cacheParameters.CultureCode,
                            cacheParameters.SiteName));

                    return ctaItem;
                },
                cacheParameters);

            return result;
        }

        /// <inheritdoc/>
        public CTAItem GetCTAItem(string nodeAliasPath)
        {
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByPathByType,
                    "NavigationCTA",
                    nodeAliasPath,
                    PageType_CTAItem.CLASS_NAME),
                IsCultureSpecific = true,
                CultureCode = this.context?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.context?.Site?.SiteName,
            };
            var result = this.cacheService.Get(
                cp =>
                {
                    // Find the first cta item under the specified node
                    var ctaItem = DocumentHelper.GetDocuments<PageType_CTAItem>()
                        .OnCurrentSite()
                        .CombineWithDefaultCulture()
                        .Path(nodeAliasPath, PathTypeEnum.Children)
                        .NestingLevel(1)
                        .Published()
                        .OrderBy("NodeOrder")
                        .TopN(1)
                        .ProjectToType<CTAItem>()
                        .FirstOrDefault();

                    // If there is a CTA item get it's url
                    if (ctaItem != null)
                    {
                        var linkedPage = DocumentHelper.GetDocuments()
                            .OnCurrentSite()
                            .CombineWithDefaultCulture()
                            .LatestVersion()
                            .WhereEquals("NodeGuid", ctaItem.LinkPage)
                            .OrderBy("NodeOrder")
                            .TopN(1)
                            .FirstOrDefault();

                        if (linkedPage != null)
                        {
                            ctaItem.Link = DocumentURLProvider.GetAbsoluteUrl(linkedPage);
                        }
                    }

                    cp.CacheDependencies.Add(
                        this.cacheService.GetCacheKey(
                            string.Format(DummyCacheKeys.PageSiteNodeAlias, cacheParameters.SiteName, nodeAliasPath),
                            cacheParameters.CultureCode,
                            cacheParameters.SiteName));

                    return ctaItem;
                },
                cacheParameters);

            return result;
        }

        /// <inheritdoc/>
        public IEnumerable<NavItem> GetAllItemsInPath(string path)
        {
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByPathByType,
                    "Navigation",
                    path,
                    PageType_CTAItem.CLASS_NAME),
                IsCultureSpecific = true,
                CultureCode = this.context?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.context?.Site?.SiteName,
            };

            var result = this.cacheService.Get(
                cp =>
                {
                    // Get path data from the supplied path.
                    ExtractPathData(path, out string[] pathSegments, out string[] paths);

                    // Query for the nodes using the list of paths and convert them to the dto. We do not want folders showing up in the navigation as Kentico excludes them
                    // from the URL
                    var pages = DocumentHelper.GetDocuments()
                        .Path(paths)
                        .OnCurrentSite()
                        .CombineWithDefaultCulture()
                        .LatestVersion()
                        .Published()
                        .WithCoupledColumns()
                        .NestingLevel(-1)
                        .OrderBy("NodeLevel")
                        .ToList()
                        .Where(d => d.ClassName != "CMS.Folder")
                        .Select(p => new NavItem()
                        {
                            DisplayTitle = p.DocumentName,
                            Link = DocumentURLProvider.GetUrl(p).TrimStart('~'),
                            NodeAliasPath = p.NodeAliasPath,
                            NodeID = p.NodeID,
                            NodeLevel = p.NodeLevel,
                            NodeOrder = p.NodeOrder,
                            NodeParentID = p.NodeParentID,
                        });

                    // consturct the cache dependencies based on the segments
                    cp.CacheDependencies = pathSegments.Select(s =>
                            this.cacheService.GetCacheKey(
                            string.Format(DummyCacheKeys.PageSiteNodeAlias, this.context?.Site?.SiteName, s),
                            cacheParameters.CultureCode,
                            cacheParameters.SiteName))
                            .ToList();

                    return pages;
                },
                cacheParameters);
            return result;
        }

        /// <inheritdoc/>
        public IEnumerable<NavItem> GetSubTreeByPath(string path)
        {
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByPathByType,
                    "Navigation",
                    path,
                    "all"),
                IsCultureSpecific = true,
                CultureCode = this.context?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.context?.Site?.SiteName,
            };

            var pageTypes = this.settingsKeyRepository.GetPagePageTypes()?.ToLower()?.Split(new char[] { ';' });

            var result = this.cacheService.Get(
                cp =>
                {
                    // Query for documents of desired page types in subtree and construct NavItems.
                    var pages = DocumentHelper.GetDocuments()
                        .Path(path, PathTypeEnum.Section)
                        .OnCurrentSite()
                        .CombineWithDefaultCulture()
                        .LatestVersion()
                        .Published()
                        .WithCoupledColumns()
                        .NestingLevel(-1)
                        .OrderBy("NodeLevel")
                        .ToList()
                        .Where(d => !d.GetBooleanValue("HideFromNavigation", false) && (pageTypes != null && pageTypes.Contains(d.ClassName.ToLower())))
                        .Select(p => new NavItem()
                        {
                            DisplayTitle = p.DocumentName,
                            Link = DocumentURLProvider.GetUrl(p).TrimStart('~'),
                            NodeAliasPath = p.NodeAliasPath,
                            NodeID = p.NodeID,
                            NodeLevel = p.NodeLevel,
                            NodeOrder = p.NodeOrder,
                            NodeParentID = p.NodeParentID,
                        });

                    // TODO dependency on path and all descendents - below is WRONG ???
                    // Get path data from the supplied path.
                    ExtractPathData(path, out string[] pathSegments, out string[] paths);

                    // construct the cache dependencies based on the segments
                    cp.CacheDependencies = pathSegments.Select(s =>
                            this.cacheService.GetCacheKey(
                            string.Format(DummyCacheKeys.PageSiteNodeAlias, this.context?.Site?.SiteName, s),
                            cacheParameters.CultureCode,
                            cacheParameters.SiteName))
                            .ToList();

                    // set parent-child relations
                    var structuredItems = StructureChildren(pages);

                    return structuredItems;
                },
                cacheParameters);

            // Get path data from the supplied path.
            ExtractPathData(path, out string[] pathSegments, out string[] paths);

            this.SetOnPathItems(paths, result);

            return result;
        }


        /// <inheritdoc/>
        public IEnumerable<NavItem> GetSectionNavigation(TreeNode currentNode)
        {
            if (currentNode == null)
            {
                return new List<NavItem>();
            }

            // Get the top level node for the section, everything will be based off of that.
            TreeNode sectionHead = currentNode.Parent;
            while (sectionHead.Parent != null && sectionHead.Parent.ClassName != "CMS.Root")
            {
                sectionHead = sectionHead.Parent;
            }

            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByKey,
                    "SectionNavigation",
                    sectionHead.NodeAliasPath,
                    PageType_NavItem.CLASS_NAME),
                IsCultureSpecific = true,
                CultureCode = this.context?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.context?.Site?.SiteName,
            };

            var pageTypes = this.settingsKeyRepository.GetPagePageTypes()?.ToLower()?.Split(new char[] { ';' });

            var result = this.cacheService.Get(
                cp =>
                {
                    var documents = DocumentHelper.GetDocuments()
                        .OnCurrentSite()
                        .CombineWithDefaultCulture()
                        .Path(sectionHead.NodeAliasPath, PathTypeEnum.Section)
                        .Published()
                        .WithCoupledColumns()
                        .LatestVersion()
                        .NestingLevel(3)
                        .OrderBy("NodeOrder")
                        .Where(d => !d.GetBooleanValue("HideFromNavigation", false) && (pageTypes != null && pageTypes.Contains(d.ClassName.ToLower())))
                        .ToList();

                    var navItems = documents
                        .Where(d => !d.GetBooleanValue("HideFromNavigation", false))
                        .Select(d =>
                            new NavItem()
                            {
                                DisplayTitle = d.DocumentName,
                                Link = DocumentURLProvider.GetAbsoluteUrl(d),
                                NodeAliasPath = d.NodeAliasPath,
                                NodeID = d.NodeID,
                                NodeLevel = d.NodeLevel,
                                NodeOrder = d.NodeOrder,
                                NodeParentID = d.NodeParentID,
                                IsClickable = true,
                                LinkPage = d.NodeGUID,
                            }).ToList();

                    if (navItems.Any())
                    {
                        // Bust the cache on the change to any of the nav items
                        cp.CacheDependencies.Add(
                            string.Format(DummyCacheKeys.PageSiteNodePathChildren, cacheParameters.SiteName, sectionHead.NodeAliasPath));
                        cp.CacheDependencies.AddRange(navItems.Select(n => string.Format(DummyCacheKeys.PageNodeId, n.NodeID)));
                    }

                    var structuredItems = StructureChildren(navItems);

                    return structuredItems;
                }, cacheParameters);

            // Get path data from the supplied path.
            ExtractPathData(currentNode.NodeAliasPath, out string[] pathSegments, out string[] paths);

            this.SetOnPathItems(paths, result);

            return result;
        }

        /// <inheritdoc/>
        public void SetActiveItems(TreeNode currentNode, IEnumerable<NavItem> navItems)
        {
            // Get path data from the supplied path.
            ExtractPathData(currentNode.NodeAliasPath, out string[] pathSegments, out string[] paths);

            this.SetOnPathItems(paths, navItems);
        }

        /// <inheritdoc />
        public void SetActiveItem(TreeNode currentNode, IEnumerable<NavItem> navItems)
        {
            foreach (var item in navItems)
            {
                if (item.LinkedPagePath == currentNode.NodeAliasPath)
                {
                    item.IsOnPath = true;
                }
                else
                {
                    item.IsOnPath = false;
                }
            }
        }

        /// <summary>
        /// Takes a node alias path and breaks it up into component data.
        /// </summary>
        /// <param name="path">
        /// The node alias path.
        /// </param>
        /// <param name="pathSegments">
        /// An array of individual segments in the path.
        /// </param>
        /// <param name="paths">
        /// The array node alias paths of each ancester.
        /// </param>
        protected static void ExtractPathData(
            string path,
            out string[] pathSegments,
            out string[] paths)
        {
            // Beak up the path into segments
            pathSegments = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            paths = new string[pathSegments.Length];

            if (pathSegments.Length > 0)
            {
                // use the segments to construct the paths of all intermediary nodes
                string pathBuilder = "/" + pathSegments[0];
                paths[0] = pathBuilder;
                for (int i = 1; i < paths.Length; i++)
                {
                    pathBuilder = pathBuilder + "/" + pathSegments[i];
                    paths[i] = pathBuilder;
                }
            }
        }

        /// <summary>
        /// Recursive method that goes through a list of nav items and their children to determine if they fall into a list of paths.
        /// </summary>
        /// <param name="paths">The array node alias paths of each ancester.</param>
        /// <param name="items">A list of Navigation Items to check.</param>
        protected void SetOnPathItems(string[] paths, IEnumerable<NavItem> items)
        {
            if (items == null || !items.Any())
            {
                return;
            }

            foreach (var item in items)
            {
                if (paths.Contains(item.NodeAliasPath) ||
                    paths.Contains(item.LinkedPagePath))
                {
                    item.IsOnPath = true;
                    this.SetOnPathItems(paths, item.Children);
                }
                else
                {
                    // Otherwise, remove it from the path,
                    // so they won't show up as additional
                    // selected items in the menu
                    item.IsOnPath = false;
                    this.SetOnPathItems(paths, item.Children);
                }
            }
        }

        /// <summary>
        /// Reorganizes the list into a hierarchical structure.
        /// </summary>
        /// <param name="navItems">original list of navigation nodes.</param>
        /// <returns>a new list of navigation nodes with children assigned.</returns>
        private static IEnumerable<NavItem> StructureChildren(IEnumerable<NavItem> navItems)
        {
            if (!navItems.Any())
            {
                return navItems;
            }

            foreach (var navItem in navItems)
            {
                navItem.Children = navItems
                    .Where(item => item.NodeParentID == navItem.NodeID)
                    .ToList();
            }

            var minNodeLevel = navItems.Min(x => x.NodeLevel);

            var topLevelDocuments = navItems
                .Where(item => item.NodeLevel == minNodeLevel)
                .ToList(); // Root Level Elements

            return topLevelDocuments;
        }

        /// <summary>
        /// Gets a list of navigation nodes by path.
        /// </summary>
        /// <param name="path">path to search.</param>
        /// <param name="nestinglevel">
        /// The nesting level.
        /// </param>
        /// <returns>list of navigation nodes.</returns>
        private IEnumerable<NavItem> GetNavItemsByPath(string path, int nestinglevel = -1)
        {
            var cacheParameters = new CacheParameters
            {
                CacheKey = string.Format(
                    DataCacheKeys.DataSetByPathByType,
                    "Navigation",
                    path,
                    PageType_NavItem.CLASS_NAME),
                IsCultureSpecific = true,
                CultureCode = this.context?.CultureName,
                IsSiteSpecific = true,
                SiteName = this.context?.Site?.SiteName,
            };

            var result = this.cacheService.Get(
                cp =>
                {
                    // Get the NavItem nodes
                    var navItems = DocumentHelper.GetDocuments<PageType_NavItem>()
                        .OnCurrentSite()
                        .CombineWithDefaultCulture()
                        .Path(path, PathTypeEnum.Children)
                        .Published()
                        .LatestVersion()
                        .NestingLevel(nestinglevel)
                        .OrderBy("NodeOrder")
                        .ProjectToType<NavItem>()
                        .ToList();

                    // Compile a list of guids that the nav items point to
                    List<Guid> pageGuidList = navItems.Where(n => !n.LinkPage.Equals(Guid.Empty)).Select(n => n.LinkPage).ToList();

                    // Perform the query to get the target pages
                    var linkedPages = DocumentHelper.GetDocuments()
                        .OnCurrentSite()
                        .CombineWithDefaultCulture()
                        .LatestVersion()
                        .WhereIn("NodeGUID", pageGuidList)
                        .ToList();

                    if (linkedPages.Any())
                    {
                        // Back fill the target urls to the nav items
                        foreach (var navItem in navItems)
                        {
                            var linkedPage = linkedPages.Where(lp => lp.NodeGUID == navItem.LinkPage).FirstOrDefault();
                            if (linkedPage != null)
                            {
                                navItem.Link = DocumentURLProvider.GetAbsoluteUrl(linkedPage);
                                navItem.LinkedPagePath = linkedPage.NodeAliasPath;
                            }
                        }
                    }

                    if (navItems.Any())
                    {
                        // Bust the cache on the change to any of the nav items
                        cp.CacheDependencies.Add(
                            string.Format(DummyCacheKeys.PageSiteNodePathChildren, cacheParameters.SiteName, path));
                        cp.CacheDependencies.AddRange(navItems.Select(n => string.Format(DummyCacheKeys.PageNodeId, n.NodeID)));
                    }

                    return navItems;
                },
                cacheParameters);
            return result;
        }
    }
}
