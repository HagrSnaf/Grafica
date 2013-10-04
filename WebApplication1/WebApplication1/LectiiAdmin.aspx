<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="LectiiAdmin.aspx.cs" Inherits="WebApplication1.LectiiAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="buttons">
	                <span class="button blue_button search_button"><span><span><a class="link_submit" href="LectieAddadmin.aspx">Adauga <img style="border: none;vertical-align: text-bottom;" src="images/plus.gif" /></a></span></span></span>
	           </div>
            <div class="options">
                <div class="options_title">Capitole</div>
            </div>
            
            <div class="table_wrapper">
                <div class="table_wrapper_inner">

                    <asp:GridView ID="lectii_grid" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="id" DataSourceID="SqlDataSource1" 
                        EmptyDataText="There are no data records to display." Width="931px">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                                SortExpression="id" />
                            <asp:BoundField DataField="nume" HeaderText="nume" SortExpression="nume" />
                            <asp:BoundField DataField="id_capitol" HeaderText="id_capitol" 
                                SortExpression="id_capitol" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="delete" runat="server" CausesValidation="false" CommandName="Sterge"
                                    Text="Sterge" CommandArgument='<%# Eval("id") %>' OnClick="sterge_lectie" />
                                    <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="Edit"
                                    Text="Editeaza" CommandArgument='<%# Eval("id") %>' OnClick="edit_lectie" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:userConnectionString %>" 
                        DeleteCommand="DELETE FROM [lectie] WHERE [id] = @id" 
                        InsertCommand="INSERT INTO [lectie] ([id], [nume], [id_capitol], [descriere], [anexa]) VALUES (@id, @nume, @id_capitol, @descriere, @anexa)" 
                        ProviderName="<%$ ConnectionStrings:userConnectionString.ProviderName %>" 
                        SelectCommand="SELECT [id], [nume], [id_capitol], [descriere], [anexa] FROM [lectie]" 
                        UpdateCommand="UPDATE [lectie] SET [nume] = @nume, [id_capitol] = @id_capitol, [descriere] = @descriere, [anexa] = @anexa WHERE [id] = @id">
                        <DeleteParameters>
                            <asp:Parameter Name="id" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="id" Type="Int32" />
                            <asp:Parameter Name="nume" Type="String" />
                            <asp:Parameter Name="id_capitol" Type="Int32" />
                            <asp:Parameter Name="descriere" Type="String" />
                            <asp:Parameter Name="anexa" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="nume" Type="String" />
                            <asp:Parameter Name="id_capitol" Type="Int32" />
                            <asp:Parameter Name="descriere" Type="String" />
                            <asp:Parameter Name="anexa" Type="String" />
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
