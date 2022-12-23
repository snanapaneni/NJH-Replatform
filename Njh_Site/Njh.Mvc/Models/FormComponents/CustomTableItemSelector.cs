using CMS.DocumentEngine;
using CMS.SiteProvider;
using Kentico.Forms.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CMS.CustomTables;
using Microsoft.AspNetCore.Mvc.Rendering;

[assembly: RegisterFormComponent("CustomTableItemSelector",
    typeof(Njh.Mvc.Models.FormComponents.CustomTableItemSelector),
    "CustomTableItemSelector", Description = "Custom table item selector", IconClass = "icon-l-list-title")]

namespace Njh.Mvc.Models.FormComponents
{
    
    public class CustomTableItemSelector : FormComponent<CustomTableItemSelectorProperties, string>
    {
        public const string IDENTIFIER = "CustomTableItemSelector";


        public override string GetValue()
        {
            if (Value == null)
            {
                return null;
            }

            return string.Join("|", Value);
        }

        public override void SetValue(string value)
        {

            Value = value?.Split('|');
        }

        [BindableProperty]
        public string[] Value
        {
            get; set;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> toReturn = new List<ValidationResult>();

            int min = Properties.Min;
            int max = Properties.Max;

            if (Value.Length > max)
            {
                toReturn.Add(new ValidationResult($"The maximum is {max}"));
            }

            if (Value.Length < min)
            {
                toReturn.Add(new ValidationResult($"The minimum is {min}"));
            }

            return toReturn;
        }

        public List<SelectListItem> Items
        {
            get
            {
                string where = !string.IsNullOrEmpty(Properties.Where) ? Properties.Where : "1=1";
                var toReturn = CustomTableItemProvider.GetItems(Properties.ClassName).Where(where)
                  .Select(item => new SelectListItem() { Text = item.GetStringValue(Properties.DisplayColumn, string.Empty), 
                                                         Value = item.GetStringValue(Properties.FieldToSave, string.Empty), 
                                                         Selected = Value != null && Value.Contains(item.ItemGUID.ToString()) });

                if (Properties.GroupByValue)
                {
                    toReturn = toReturn.GroupBy(x => x.Value).Select(g => g.First());
                }

                toReturn = toReturn.OrderBy(x => x.Text);

                return toReturn.ToList();
            }
        }
    }
}
