<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Capitoadmin.aspx.cs" Inherits="WebApplication1.Capitoadmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
             <div class="buttons">
	                <span class="button blue_button search_button"><span><span><a class="link_submit" href="CapitolAddadmin.aspx">Adauga <img style="border: none;vertical-align: text-bottom;" src="images/plus.gif" /></a></span></span></span>
	           </div>
            <div class="options">
                <div class="options_title">Capitole</div>
            </div>
            
            <div class="table_wrapper">
                <div class="table_wrapper_inner">
                    <asp:GridView ID="capitole_grid" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="id" DataSourceID="SqlDataSource1" 
                        EmptyDataText="There are no data records to display." Width="940px" 
                        AllowSorting="True">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                                SortExpression="id" />
                            <asp:BoundField DataField="nume" HeaderText="nume" SortExpression="nume" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="delete" runat="server" CausesValidation="false" CommandName="Sterge"
                                    Text="Sterge" CommandArgument='<%# Eval("id") %>' OnClick="sterge_capitol" />
                                    <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="Edit"
                                    Text="Editeaza" CommandArgument='<%# Eval("id") %>' OnClick="edit_capitol" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:userConnectionString %>" 
                        DeleteCommand="DELETE FROM [capitol] WHERE [id] = @id" 
                        InsertCommand="INSERT INTO [capitol] ([id], [nume], [descriere]) VALUES (@id, @nume, @descriere)" 
                        ProviderName="<%$ ConnectionStrings:userConnectionString.ProviderName %>" 
                        SelectCommand="SELECT [id], [nume], [descriere] FROM [capitol]" 
                        UpdateCommand="UPDATE [capitol] SET [nume] = @nume, [descriere] = @descriere WHERE [id] = @id">
                        <DeleteParameters>
                            <asp:Parameter Name="id" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="id" Type="Int32" />
                            <asp:Parameter Name="nume" Type="String" />
                            <asp:Parameter Name="descriere" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="nume" Type="String" />
                            <asp:Parameter Name="descriere" Type="String" />
                            <asp:Parameter Name="id" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
             </div>
            </div>
            <div class="options">
                <div class="options_title">Capitole</div>
                <div class="options_filtru">
                    &nbsp;
                </div>
            </div>
</asp:Content>
