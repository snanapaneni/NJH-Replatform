using CMS.Core;
using CMS.DataEngine;
using CMS.Helpers;
using Njh.Kernel.Extensions;
using Njh.Kernel.Services;

namespace Njh.Mvc.Models.PageTemplateProperties
{
    public class PressReleaseTemplateProperties : PageTemplatePropertiesBase
    {
        private readonly ISettingsKeyRepository settingsKeyRepository;

        public PressReleaseTemplateProperties()
        {
            settingsKeyRepository = Service.Resolve<ISettingsKeyRepository>();
        }

        public string MediaContactsContent => this.settingsKeyRepository.GetMediaContacts();

        public string MediaResourcesContent => this.settingsKeyRepository.GetMediaResources();

        public string MediaContactsContentTitle => ResHelper.GetString("NJH.PressRelease.MediaContactsContentTitle");

        public string MediaResourcesContentTitle => ResHelper.GetString("NJH.PressRelease.MediaResourcesContentTitle");

        public string BoilerPlateRTEDefaultCopy => ResHelper.GetString("NJH.PressRelease.BoilerPlateRTEDefaultCopy");
    }
}
