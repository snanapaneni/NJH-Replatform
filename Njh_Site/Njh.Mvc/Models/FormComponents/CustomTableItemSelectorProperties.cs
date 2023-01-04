using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;

namespace Njh.Mvc.Models.FormComponents
{
    public class CustomTableItemSelectorProperties : FormComponentProperties<string>
    {
        public CustomTableItemSelectorProperties() : base(FieldDataType.Text, 2000)
        {
        }

        public string ClassName
        {
            get; set;
        }

        public string DisplayColumn
        {
            get; set;
        }

        public string FieldToSave
        {
            get; set;
        }

        public string Where
        {
            get; set;
        }

        public bool GroupByValue { get; set; }

        public int Min { get; set; } = 0;
        public int Max { get; set; } = Int32.MaxValue;
        public bool Multiple { get; set; }
        public override string DefaultValue { get; set; }
    }
}
