using ReasonOne.AspNetCore.Mvc.ViewComponents;

namespace Njh.Mvc.Components.RelatedDocuments
{
    public class PrimaryConditionViewComponent : SafeViewComponent<PrimaryConditionViewComponent>
    {
        /// <inheritdoc />
        public PrimaryConditionViewComponent(ILogger<PrimaryConditionViewComponent> logger, IViewComponentErrorVisibility viewComponentErrorVisibility)
            : base(logger, viewComponentErrorVisibility)
        {
        }
    }
}
