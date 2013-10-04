<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CapitolEditadmin.aspx.cs" Inherits="WebApplication1.CapitolEditadmin" validateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
    function intOnly(i) {
        if (i.value.length > 0) {
            i.value = i.value.replace(/[^\d]+/g, '');
        }
    }
</script>
            <div class="buttons">
                <span class="button blue_button search_button"><span><span>Salveaza <img style="vertical-align: text-bottom;" src="images/save.png" alt="save" /></span></span><asp:Button
                    ID="salveaza" runat="server" Text="Salveaza" onclick="salveaza_Click" /></span>
                <span class="button blue_button search_button"><span><span>Anuleaza <img style="vertical-align: text-bottom;" src="images/cancel.png" alt="cancel" /></span></span><asp:Button
                    ID="cancel" runat="server" Text="Anuleaza" onclick="cancel_Click" /></span>
            </div>
            <div class="gray_bar">
            	Editare Capitol            
            </div>
            <div class="container_inputs">
                <div class='inputs'>
                    <div class='inputs_left'>

                        <label>Nume</label>
                        <asp:TextBox ID="nume" runat="server" AutoPostBack="false"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="validator1" runat="server" ControlToValidate="nume"  ValidationGroup="reggrup"
                          Display="Dynamic" ErrorMessage="Numele este obligatoriu"></asp:RequiredFieldValidator>

                    </div>
                    <div class="imputs_center">
                        <label>Numar ordine</label>
                        <asp:TextBox ID="nr_ord" runat="server" onChange="intOnly(this);" onKeyUp="intOnly(this);" onKeyPress="intOnly(this);"></asp:TextBox>
                    </div>
                </div>
                <div class='inputs'>
                    <label>Descriere</label>
                    <asp:TextBox ID="descriere" runat="server" AutoPostBack="false" ></asp:TextBox>
                </div>
         <div class='inputs'>
            <label>Imagine:</label>
            <asp:FileUpload ID="FileUpload1" runat="server" Height="21px" Width="251px" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" 
                OnClick="btnUpload_Click" Height="21px" Width="109px" />
        </div>
   </div>
</asp:Content>
