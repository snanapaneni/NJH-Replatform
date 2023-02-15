using CMS.Core;
using CMS.Helpers;
using Njh.Kernel.Extensions;
using Njh.Kernel.Services;

namespace Njh.Mvc.Models.PageTemplateProperties
{
    public class SpecialtyMainTemplateProperties : PageTemplatePropertiesBase
    {
        private readonly ISettingsKeyRepository settingsKeyRepository;

        public SpecialtyMainTemplateProperties()
        {
            settingsKeyRepository = Service.Resolve<ISettingsKeyRepository>();
        }

        public string MakeAnAppointmentText => this.settingsKeyRepository.GetMakeAnAppointmentText();

        public string MakeAnAppointmentUrl => this.settingsKeyRepository.GetMakeAnAppointmentPage();

        public string GlobalPhoneNumber => this.settingsKeyRepository.GetGlobalPhoneNumber();

        public string GlobalPhoneNumberText => this.settingsKeyRepository.GetGlobalPhoneNumberText();

        public string CallText => ResHelper.GetString("NJH.SpecialtyPage.CallText");
    }
}
