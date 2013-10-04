<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ItemEditadmin.aspx.cs" Inherits="WebApplication1.ItemEditadmin" validateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="buttons">
                <span class="button blue_button search_button"><span><span>Salveaza <img style="vertical-align: text-bottom;" src="images/save.png" alt="save" /></span></span><asp:Button
                    ID="salveaza" runat="server" Text="Salveaza" onclick="salveaza_Click" /></span>
                <span class="button blue_button search_button"><span><span>Anuleaza <img style="vertical-align: text-bottom;" src="images/cancel.png" alt="cancel" /></span></span><asp:Button
                    ID="cancel" runat="server" Text="Anuleaza" onclick="cancel_Click" /></span>
            </div>
            <div class="gray_bar">
            	Editare Item
            </div>
            <div class="container_inputs">
                <div class='inputs'>
                    <div class='inputs_left'>
                        <label>Tip test</label>
                        <asp:DropDownList ID="tip" runat="server" 
                            onselectedindexchanged="tip_SelectedIndexChanged"  AutoPostBack="true" >
                              <asp:ListItem Value="Capitol"></asp:ListItem>
                              <asp:ListItem Value="Lectie"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class='inputs_center'>
                        <label>Lectie/Capitol</label>
                        <asp:DropDownList ID="iduri" runat="server" 
                            DataSourceID="SqlDataSource1" DataTextField="nume" DataValueField="nume">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:userConnectionString %>" 
                            SelectCommand="SELECT [nume] FROM [capitol]"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:userConnectionString %>" 
                            SelectCommand="SELECT [nume] FROM [lectie]"></asp:SqlDataSource>
                    </div>
                    <div class='inputs_right'>
                        <label>Raspuns</label>
                        <asp:DropDownList ID="raspuns" runat="server" 
                            onselectedindexchanged="tip_SelectedIndexChanged" >
                              <asp:ListItem Value="a"></asp:ListItem>
                              <asp:ListItem Value="b"></asp:ListItem>
                              <asp:ListItem Value="c"></asp:ListItem>
                              <asp:ListItem Value="d"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class='inputs'>
                    <label>Enunt</label>
                    <asp:TextBox ID="enunt" runat="server"></asp:TextBox>
                </div>
                <div class='inputs'>
                    <label>Imagine enunt:</label>
                    <asp:FileUpload ID="FileUpload1" runat="server" Height="21px" Width="251px" />
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" 
                        OnClick="btnUpload_Click" Height="21px" Width="109px" />
                </div>
                <div class='inputs'>
                    <label>Varianta a</label>
                    <asp:TextBox ID="var_a" runat="server"></asp:TextBox>
                </div>
                <div class='inputs'>
                    <label>Imagine varianta a:</label>
                    <asp:FileUpload ID="FileUpload2" runat="server" Height="21px" Width="251px" />
                    <asp:Button ID="Button1" runat="server" Text="Upload" Height="21px" 
                        Width="109px" onclick="Button1_Click" />
                </div>
                <div class='inputs'>
                    <label>Varianta b</label>
                    <asp:TextBox ID="var_b" runat="server"></asp:TextBox>
                </div>
                <div class='inputs'>
                    <label>Imagine varianta b:</label>
                    <asp:FileUpload ID="FileUpload3" runat="server" Height="21px" Width="251px" />
                    <asp:Button ID="Button2" runat="server" Text="Upload" Height="21px" 
                        Width="109px" onclick="Button2_Click" />
                </div>
                <div class='inputs'>
                    <label>Varianta c</label>
                    <asp:TextBox ID="var_c" runat="server"></asp:TextBox>
                </div>
                <div class='inputs'>
                    <label>Imagine varianta c:</label>
                    <asp:FileUpload ID="FileUpload4" runat="server" Height="21px" Width="251px" />
                    <asp:Button ID="Button3" runat="server" Text="Upload" Height="21px" 
                        Width="109px" onclick="Button3_Click" />
                </div>
                <div class='inputs'>
                    <label>Varianta d</label>
                    <asp:TextBox ID="var_d" runat="server"></asp:TextBox>
                </div>
                <div class='inputs'>
                    <label>Imagine varianta d:</label>
                    <asp:FileUpload ID="FileUpload5" runat="server" Height="21px" Width="251px" />
                    <asp:Button ID="Button4" runat="server" Text="Upload" Height="21px" 
                        Width="109px" onclick="Button4_Click" />
                </div>
             
            </div>
</asp:Content>
