using Njh.Kernel.Models;
using Njh.Kernel.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Services
{
    internal class ReviewerService : ServiceBase, IReviewerService
    {
        private readonly ContextConfig contextConfig;

        private readonly ICacheService cacheService;
        private readonly IPhysicianService physicianService;
        private readonly IPageService pageService;
        private readonly IProfessionalTitleService professionalTitleService;



        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ReviewerService"/> class.
        /// </summary>
        /// <param name="contextConfig">
        /// The context config.
        /// </param>
        /// <param name="cacheService">
        /// The cache service.
        /// </param>
        public ReviewerService(
            ContextConfig contextConfig,
            ICacheService cacheService,
            IPhysicianService physicianService,
            IPageService pageService,
            IProfessionalTitleService professionalTitleService)
        {
            this.contextConfig = contextConfig ??
                throw new ArgumentNullException(nameof(contextConfig));

            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));

            this.physicianService = physicianService ??
                throw new ArgumentNullException(nameof(physicianService));

            this.pageService = pageService ??
                throw new ArgumentNullException(nameof(pageService));

            this.professionalTitleService = professionalTitleService ??
                throw new ArgumentNullException(nameof(professionalTitleService));
        }
        public Reviewers GetReviewersByNodeGuid(Guid nodeGuid)
        {
            var page = pageService.GetDocument(nodeGuid, !contextConfig.IsPreview);
            var reviewers = new Reviewers();
            reviewers.HideUrl = page?.Hide_URL ?? false;
            reviewers.ReviewDate = page?.ReviewedDate ?? DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(page?.ReviewedBy))
            {
                reviewers.ReviewersList.Add(new Reviewer(page.ReviewedBy));
            }

            if (!string.IsNullOrWhiteSpace(page?.ReviewerListGUID))
            {
                var reviewersGuids = page.ReviewerListGUID
                    .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => Guid.Parse(s))
                    .ToList();
                var physiciansList = physicianService.GetPhysiciansByGuids(published: false, cached: false,
                                                                          physiciansGuids: reviewersGuids.ToArray());
                var reviewersTemp = physiciansList.Select(p => new Reviewer()
                {
                    Name = p.PhysicianDisplayName,
                    Url = p.AbsoluteURL,
                    Titles = professionalTitleService?
                                .GetProfessionalTitlesByGuids(p?.ProfessionalTitles
                                                                  .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                                                                    .Select(s => Guid.Parse(s))
                                                                    .ToArray())
                });
                reviewers.ReviewersList.AddRange(reviewersTemp);
            }

            return reviewers;

        }
    }
}
