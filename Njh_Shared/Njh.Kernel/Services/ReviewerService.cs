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
            IPhysicianService physicianService)
        {
            this.contextConfig = contextConfig ??
                throw new ArgumentNullException(nameof(contextConfig));

            this.cacheService = cacheService ??
                throw new ArgumentNullException(nameof(cacheService));

            this.physicianService = physicianService ??
                throw new ArgumentNullException(nameof(physicianService));
        }
        public IEnumerable<Reviewer> GetReviewersByNodeGuid(Guid nodeGuid)
        {
            //var reviewersRaw = 
            //var physician = physicianService.GetPhysiciansByGuids();
            throw new NotImplementedException();
        }
    }
}
