using Microsoft.AspNetCore.Mvc;
using Njh.Kernel.Extensions;
using Njh.Kernel.Services;
using Njh.Mvc.Models;
using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.Alert
{
    /// <summary>
    /// Alert view component
    /// </summary>
    public class AlertViewComponent
        : SafeViewComponent<AlertViewComponent>
    {
        private readonly IAlertService alertService;
        private readonly ISettingsKeyRepository settingsKeyRepository;

        /// <summary>
        /// Initializes a new instance of the<see cref="AlertViewComponent"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="viewComponentErrorVisibility">Error Visibility.</param>
        /// <param name="alertService">Alert Service.</param>
        /// <param name="settingsKeyRepository">Settings Key Repo.</param>
        public AlertViewComponent(ILogger<AlertViewComponent> logger, IViewComponentErrorVisibility viewComponentErrorVisibility, IAlertService alertService, ISettingsKeyRepository settingsKeyRepository)
            : base(logger, viewComponentErrorVisibility)
        {
            this.alertService = alertService;
            this.settingsKeyRepository = settingsKeyRepository;
        }

        /// <summary>
        /// Renders the view component markup.
        /// </summary>
        /// <returns>
        /// The view component result.
        /// </returns>
        public IViewComponentResult Invoke()
        {
            return this.TryInvoke(vc =>
            {
                AlertViewModel? model = null;
                var alert = alertService.FirstOrDefault(settingsKeyRepository.GetAlertPath());
                if (alert != null)
                {
                    model = new AlertViewModel
                    {
                        Title = alert.Title,
                        Description = alert.Description,
                    };
                }

                return vc.View("~/Views/Shared/Alert/_Alert.cshtml", model);
            });
        }
    }
}
