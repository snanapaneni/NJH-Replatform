using CMS.Core;
using CMS.MacroEngine;
using Njh.Kernel.Extensions;
using Njh.Kernel.Services;

namespace Njh.Mvc.Helpers
{
    public static class Seo
    {
        public static string GetFormattedPageTitle(ISettingsKeyRepository settingsKeyRepository, string title = "", string titleOverride = "")
        {
            string pageTitleFormatted = string.Empty;
            try
            {
                MacroResolver m = new MacroResolver();
                Dictionary<string, object> st = new Dictionary<string, object>();

                var titleFormat = settingsKeyRepository.GetPageTitleFormat();
                var titlePrefix = settingsKeyRepository.GetPageTitlePrefix();
                var pageTitle = string.IsNullOrWhiteSpace(titleOverride) ? title : titleOverride;
                st.Add("prefix", titlePrefix);
                st.Add("pagetitle_orelse_name", pageTitle);
                m.SetNamedSourceData(data: st, isPrioritized: true);
                pageTitleFormatted = m.ResolveMacros(titleFormat);

            }
            catch (Exception ex)
            {
                var logService = Service.Resolve<IEventLogService>();
                logService.LogException(source:"SeoHelper", eventCode:"GetFormattedPageTitle", ex);

                return title;
                
            }
            
            return pageTitleFormatted;
        }
    }
}
