<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Itemadmin.aspx.cs" Inherits="WebApplication1.Itemadmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="buttons">
	                <span class="button blue_button search_button"><span><span><a class="link_submit" href="ItemAddadmin.aspx">Adauga <img style="border: none;vertical-align: text-bottom;" src="images/plus.gif" /></a></span></span></span>
	           </div>
            <div class="options">
                <div class="options_title">Itemi</div>
            </div>
            
            <div class="table_wrapper">
                <div class="table_wrapper_inner">
                    
                    <asp:GridView ID="itemi_grid" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="id" DataSourceID="SqlDataSource1" 
                        EmptyDataText="There are no data records to display." Width="936px">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                                SortExpression="id" />
                            <asp:BoundField DataField="raspuns" HeaderText="raspuns" 
                                SortExpression="raspuns" />
                            <asp:BoundField DataField="tip" HeaderText="tip" SortExpression="tip" />
                            <asp:BoundField DataField="id_continut" HeaderText="id_continut" 
                                SortExpression="id_continut" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="delete" runat="server" CausesValidation="false" CommandName="Sterge"
                                    Text="Sterge" CommandArgument='<%# Eval("id") %>' OnClick="sterge_item" />
                                    <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="Edit"
                                    Text="Editeaza" CommandArgument='<%# Eval("id") %>' OnClick="edit_item" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:userConnectionString %>" 
                        DeleteCommand="DELETE FROM [item] WHERE [id] = @id" 
                        InsertCommand="INSERT INTO [item] ([id], [enunt], [a], [b], [c], [d], [raspuns], [tip], [id_continut]) VALUES (@id, @enunt, @a, @b, @c, @d, @raspuns, @tip, @id_continut)" 
                        ProviderName="<%$ ConnectionStrings:userConnectionString.ProviderName %>" 
                        SelectCommand="SELECT [id], [enunt], [a], [b], [c], [d], [raspuns], [tip], [id_continut] FROM [item]" 
                        UpdateCommand="UPDATE [item] SET [enunt] = @enunt, [a] = @a, [b] = @b, [c] = @c, [d] = @d, [raspuns] = @raspuns, [tip] = @tip, [id_continut] = @id_continut WHERE [id] = @id">
                        <DeleteParameters>
                            <asp:Parameter Name="id" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="id" Type="Int32" />
                            <asp:Parameter Name="enunt" Type="String" />
                            <asp:Parameter Name="a" Type="String" />
                            <asp:Parameter Name="b" Type="String" />
                            <asp:Parameter Name="c" Type="String" />
                            <asp:Parameter Name="d" Type="String" />
                            <asp:Parameter Name="raspuns" Type="String" />
                            <asp:Parameter Name="tip" Type="String" />
                            <asp:Parameter Name="id_continut" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="enunt" Type="String" />
                            <asp:Parameter Name="a" Type="String" />
                            <asp:Parameter Name="b" Type="String" />
                            <asp:Parameter Name="c" Type="String" />
                            <asp:Parameter Name="d" Type="String" />
                            <asp:Parameter Name="raspuns" Type="String" />
                            <asp:Parameter Name="tip" Type="String" />
                            <asp:Parameter Name="id_continut" Type="Int32" />
                            <asp:Parameter Name="id" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    
             </div>
            </div>
            <div class="options">
                <div class="options_title">Itemi</div>
                <div class="options_filtru">
                    &nbsp;
                </div>
            </div>
</asp:Content>
