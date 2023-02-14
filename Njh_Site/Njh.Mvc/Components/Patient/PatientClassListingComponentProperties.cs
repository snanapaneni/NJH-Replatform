using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using System.Reflection.Emit;

namespace Njh.Mvc.Components.Patient
{
    /// <summary>
    /// Properties for the NJH Patient Class Listing widget.
    /// </summary>
    public class PatientClassListingComponentProperties : IWidgetProperties
    {
        /// <summary>
        /// Gets or sets the path to the Patient Education documents in the CMS Tree.
        /// </summary>
        [EditingComponent(PathSelector.IDENTIFIER, Order = 1, Label = "{$NJH.PathSelector.Properties.Label$}", ExplanationText = "{$NJH.PathSelector.Properties.Explanation$}")]
        [EditingComponentProperty(nameof(PathSelector.MaxPagesLimit), 1)]
        public IEnumerable<PathSelectorItem> ClassesPaths { get; set; } = Enumerable.Empty<PathSelectorItem>();

    }
}
