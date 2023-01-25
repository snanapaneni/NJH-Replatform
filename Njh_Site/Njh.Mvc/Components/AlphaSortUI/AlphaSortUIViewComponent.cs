// Copyright (c) NJH. All rights reserved.

using Njh.Mvc.Components.AlphaSortUI;

[assembly: Kentico.PageBuilder.Web.Mvc.RegisterWidget(
    AlphaSortUIViewComponent.Identifier,
    typeof(AlphaSortUIViewComponent),
    AlphaSortUIViewComponent.Name,
    typeof(AlphaSortUIWidgetProperties),
    Description = AlphaSortUIViewComponent.Description,
    IconClass = Njh.Mvc.Models.Constants.IconConstants.OneColumn)]

namespace Njh.Mvc.Components.AlphaSortUI
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CMS.DocumentEngine;
    using Kentico.PageBuilder.Web.Mvc;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Njh.Kernel.Definitions;
    using Njh.Kernel.Models.Dto;
    using Njh.Kernel.Services;
    using Njh.Mvc.Extensions;
    using Njh.Mvc.Helpers;
    using ReasonOne.AspNetCore.Mvc.ViewComponents;
    public class AlphaSortUIViewComponent : SafeViewComponent<AlphaSortUIViewComponent>
    {
        /// <summary>
        /// The widget identifier.
        /// </summary>
        public const string Identifier =
            "Njh.AlphaSortUI";

        /// <summary>
        /// The widget name.
        /// </summary>
        public const string Name =
            "Alpha Sort UI";

        /// <summary>
        /// The widget description.
        /// </summary>
        public const string Description =
            "Display the Alpha Sort UI widget.";

        private readonly ITreeNodeService treeNodeService;
        private readonly ISettingsKeyRepository settingsKeyRepository;
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AlphaSortUIViewComponent"/> class.
        /// </summary>
        /// <param name="treeNodeService">
        /// The treeNode service.
        /// </param>
        /// <param name="settingsKeyRepository">
        /// The settings key repository.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="viewComponentErrorVisibility">
        /// The view component error visibility.
        /// </param>
        public AlphaSortUIViewComponent(
            ISettingsKeyRepository settingsKeyRepository,
            ILogger<AlphaSortUIViewComponent> logger,
            ITreeNodeService treeNodeService,
            IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
            this.treeNodeService = treeNodeService ??
                throw new ArgumentNullException(nameof(treeNodeService));

            this.settingsKeyRepository = settingsKeyRepository ??
                throw new ArgumentNullException(nameof(settingsKeyRepository));
        }

        /// <summary>
        /// Renders the widget markup.
        /// </summary>
        /// <param name="widgetProperties">
        /// The widget properties.
        /// </param>
        /// <returns>
        /// The view component result.
        /// </returns>
        public IViewComponentResult Invoke(
            ComponentViewModel<AlphaSortUIWidgetProperties> widgetProperties)
        {
            return
                this.TryInvoke((vc) =>
                {
                    var properties = widgetProperties
                        .GetPropertiesOrDefault();

                    // Get object categories
                    var categoriesGuids = properties.ItemsCategories.Select(item => item.ObjectGuid).ToArray();

                    // Get Object Page types
                    var pageTypes = properties.ItemsPageTypes.Select(item => item.ObjectCodeName);

                    // get path 
                    var path = properties.PageItemsFolderPath?.FirstOrDefault()?.NodeAliasPath ?? string.Empty;


                    var nestingLevel = properties.IncludeAllChildren ? -1 : 1;

                    widgetProperties.CacheDependencies.CacheKeys =
                        new List<string>() { $"node|{GlobalConstants.SiteCodeName}|path|childnodes" };


                    IEnumerable<TreeNode> treeNodes = Enumerable.Empty<TreeNode>();
                    SortedDictionary<char, List<SimpleLink>> results = new SortedDictionary<char, List<SimpleLink>>();
                    if (!string.IsNullOrEmpty(path))
                    {

                        treeNodes = this.treeNodeService.GetDocumentsByCategories(
                            path,
                            pageTypes,
                            columns: new List<string> { "DocumentName", "PageName", "Hide_Url", "NodeAliasPath" },
                            categoriesGuids: categoriesGuids,
                            orderBy: "PageName ASC",
                            level: nestingLevel);
                        results = AlphaSort.GetAlphaSortedPages(treeNodes);
                    }

                    return vc.View(results);
                });
        }
    }
}
