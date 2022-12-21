using CMS.DataEngine;
using CMS.FormEngine.Web.UI;
using CMS.Helpers;
using CMS.MacroEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace CMSApp.NJH.FormControls
{
    public partial class MultipleChoiceListBoxes : FormEngineUserControl
    {
        #region "Variables"

        //private string[] selectedValues = null;

        #endregion


        #region "Properties"

        /// <summary>
        /// Gets or sets the enabled state of the control.
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return list.Enabled;
            }
            set
            {
                list.Enabled = value;
            }
        }


        /// <summary>
        /// Gets or sets form control value.
        /// </summary>
        public override object Value
        {
            get
            {
                return hidIn.Value;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                hidIn.Value = value.ToString();
                //selectedValues = ValidationHelper.GetString(value, "").Split('|');
            }
        }


        /// <summary>
        /// Gets or sets form control value.
        /// </summary>
        public object ValueOLD
        {
            get
            {
                StringBuilder text = new StringBuilder();
                foreach (ListItem item in list.Items)
                {
                    if (item.Selected)
                    {
                        text.Append(item.Value + "|");
                    }
                }
                return text.ToString().TrimEnd('|');
            }
            set
            {

            }
        }

        /// <summary>
        /// Returns selected value display names separated with comma.
        /// </summary>
        public override string ValueDisplayName
        {
            get
            {
                StringBuilder text = new StringBuilder();
                bool first = true;
                foreach (ListItem item in list.Items)
                {
                    if (item.Selected)
                    {
                        if (!first)
                        {
                            text.Append(", ");
                        }
                        text.Append(item.Text);
                        first = false;
                    }
                }
                return text.ToString();
            }
        }

        #endregion


        #region "Methods"

        protected void Page_Load(object sender, EventArgs e)
        {

            LoadAndSelectList();

            CheckRegularExpression = true;
            CheckFieldEmptiness = true;
        }


        /// <summary>
        /// Loads and selects control.
        /// </summary>
        private void LoadAndSelectList()
        {
            string options = ValidationHelper.GetString(GetValue("options"), null);
            string query = ValidationHelper.GetString(GetValue("query"), null);

            ListBoxOut.Items.Clear();
            list.Items.Clear();

            try
            {
                //FormHelper.LoadItemsIntoList(options, query, list.Items, FieldInfo);
                //FormHelper.List
                //FormHelper.LoadItemsIntoList(options, query, ListBoxOut.Items, FieldInfo);

                if (options != null)
                {
                    string[] separators = { "\r\n" };
                    string[] option = options.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
                    List<ListItem> list = new List<ListItem>();
                    foreach (string o in option)
                    {
                        ListItem li = new ListItem(o, o);
                        list.Add(li);
                    }
                    ListBoxOut.DataSource = list;
                }
                else if (query != null)
                {
                    GeneralConnection cn = ConnectionHelper.GetConnection();
                    QueryParameters items = new QueryParameters(MacroContext.CurrentResolver.ResolveMacros(query), null, QueryTypeEnum.SQLQuery, false);
                    ListBoxOut.DataSource = cn.ExecuteQuery(items);
                }

                ListBoxOut.DataBind();
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }

            //FormHelper.SelectMultipleValues(selectedValues, list.Items, ListSelectionMode.Multiple);
            //FormHelper.SelectMultipleValues(ValidationHelper.GetString(hidIn.Value, "").Split('|'), ListBoxOut.Items, ListSelectionMode.Multiple);
            //move the selected items out of ListBoxOut and ListBoxIn
            // hidIn.Value = "";

            List<ListItem> delList = new List<ListItem>();


            string[] sList = ValidationHelper.GetString(hidIn.Value, "").Split('|');

            foreach (string item in sList)
            {
                if (ListBoxOut.Items.FindByValue(item) != null)
                {
                    delList.Add(ListBoxOut.Items.FindByValue(item));
                    list.Items.Add(ListBoxOut.Items.FindByValue(item));
                }
            }

            foreach (ListItem item in delList)
            {
                ListBoxOut.Items.Remove(item);
            }

            rep.DataSource = list.Items;
            rep.DataBind();

            repOut.DataSource = ListBoxOut.Items;
            repOut.DataBind();
        }


        /// <summary>
        /// Displays exception control with current error.
        /// </summary>
        /// <param name="ex">Thrown exception</param>
        private void DisplayException(Exception ex)
        {
            //FormControlError ctrlError = new FormControlError();
            //ctrlError.InnerException = ex;
            //Controls.Add(ctrlError);
            //Response.Write("<!--CAPTURED_ERROR" + ex.Message + "-->");
            list.Visible = false;
        }

        #endregion

    }
}