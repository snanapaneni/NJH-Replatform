<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultipleChoiceListBoxes.ascx.cs" Inherits="CMSApp.NJH.FormControls.MultipleChoiceListBoxes" %>

<script type='text/javascript' src='https://code.jquery.com/jquery-2.1.4.js'></script>
<script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js" type="text/javascript"></script>

<div class="CustomControlMultipleChoiceListBoxes">
<ul id="a" runat="server">
    <asp:Repeater ID="repOut" runat="server">
        <ItemTemplate>
            <li rel="<%# Eval("Value") %>"><%# Eval("Text") %></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>

<asp:ListBox Visible="false" ID="ListBoxOut" DataTextField="Text" DataValueField="Value" CssClass="pickerOut" runat="server" Style="width: 100px; height: 100px;" SelectionMode="Multiple"></asp:ListBox>

<ul id="b" runat="server"> 
    <asp:Repeater ID="rep" runat="server">
        <ItemTemplate>
            <li rel="<%# Eval("Value") %>"><%# Eval("Text") %></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>

<asp:ListBox Visible="false" ID="list" CssClass="pickerIn" DataTextField="Text" DataValueField="Value" runat="server" Style="width: 100px; height: 100px;" SelectionMode="Multiple"></asp:ListBox>
<asp:HiddenField ID="hidIn" runat="server" />

</div>
<style>
    .CustomControlMultipleChoiceListBoxes ul
    {
        border: 1px solid Black;
        width: 200px;
        height: 400px;
        overflow: scroll;
        display: inline-block;
        vertical-align: top;
        padding-left: 10px;
    }
    .CustomControlMultipleChoiceListBoxes li
    {
        /*background-color: Azure;*/
        border-bottom: 1px dotted Gray;
        list-style: none;
        list-style-type: none;
        /*margin-left: 0px;
        padding-left: 0px;*/
    }
        .CustomControlMultipleChoiceListBoxes li.selected
        {
            background-color: steelblue;
        }
</style>

<script>
    jQuery("#<%=b.ClientID%>,#<%=a.ClientID%>").on('click', 'li', function (e) {
        if (e.ctrlKey || e.metaKey) {
            jQuery(this).toggleClass("selected");
        } else {
            jQuery(this).addClass("selected").siblings().removeClass('selected');
        }
    }).sortable({
        connectWith: "ul",
        delay: 150, //Needed to prevent accidental drag when trying to select
        revert: 0,
        helper: function (e, item) {
            var helper = jQuery('<li/>');
            if (!item.hasClass('selected')) {
                item.addClass('selected').siblings().removeClass('selected');
            }
            var elements = item.parent().children('.selected').clone();
            item.data('multidrag', elements).siblings('.selected').remove();
            return helper.append(elements);
        },
        stop: function (e, info) {
            info.item.after(info.item.data('multidrag')).remove();
            //remove and add item in the base list boxes too  
            //jQuery(".pickerOut").children().remove();
            //jQuery(".pickerIn").children().remove();
            //jQuery("#a li").each(function () {
            // jQuery(".pickerOut").append("<option value='" + jQuery(this).attr("rel") + "'>" + jQuery(this).html() + "</option>");
            //});
            jQuery("#<%=hidIn.ClientID%>").val("");
            jQuery("#<%=b.ClientID%> li").each(function () {
                if (jQuery("#<%=hidIn.ClientID%>").val() == "") {
                    jQuery("#<%=hidIn.ClientID%>").val(jQuery(this).attr("rel"));
                }
                else {
                    jQuery("#<%=hidIn.ClientID%>").val(jQuery("#<%=hidIn.ClientID%>").val() + "|" + jQuery(this).attr("rel"));
                }
            });
        }
    });
</script>
