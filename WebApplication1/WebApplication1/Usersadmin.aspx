<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Usersadmin.aspx.cs" Inherits="WebApplication1.Usersadmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="options">
                <div class="options_title">utilizatori</div>
            </div>
            
            <div class="table_wrapper">
                <div class="table_wrapper_inner">
                   
                    <asp:GridView ID="utilizatori_grid" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="id" DataSourceID="SqlDataSourceUsers" 
                        EmptyDataText="There are no data records to display." Width="939px">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                                SortExpression="id" />
                            <asp:BoundField DataField="nume" HeaderText="nume" SortExpression="nume" />
                            <asp:BoundField DataField="prenume" HeaderText="prenume" 
                                SortExpression="prenume" />
                            <asp:BoundField DataField="username" HeaderText="username" 
                                SortExpression="username" />
                            <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="delete" runat="server" CausesValidation="false" CommandName="SendMail"
                                    Text="Sterge" CommandArgument='<%# Eval("id") %>' OnClick="sterge_utilizator" />
                            </ItemTemplate>
        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSourceUsers" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:userConnectionString %>" 
                        DeleteCommand="DELETE FROM [user] WHERE [id] = @id" 
                        InsertCommand="INSERT INTO [user] ([id], [nume], [prenume], [username], [parola], [data_nastere], [admin], [email]) VALUES (@id, @nume, @prenume, @username, @parola, @data_nastere, @admin, @email)" 
                        ProviderName="<%$ ConnectionStrings:userConnectionString.ProviderName %>" 
                        SelectCommand="SELECT [id], [nume], [prenume], [username], [parola], [data_nastere], [admin], [email] FROM [user]" 
                        UpdateCommand="UPDATE [user] SET [nume] = @nume, [prenume] = @prenume, [username] = @username, [parola] = @parola, [data_nastere] = @data_nastere, [admin] = @admin, [email] = @email WHERE [id] = @id">
                        <DeleteParameters>
                            <asp:Parameter Name="id" Type="Int32" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="id" Type="Int32" />
                            <asp:Parameter Name="nume" Type="String" />
                            <asp:Parameter Name="prenume" Type="String" />
                            <asp:Parameter Name="username" Type="String" />
                            <asp:Parameter Name="parola" Type="String" />
                            <asp:Parameter DbType="Date" Name="data_nastere" />
                            <asp:Parameter Name="admin" Type="Int32" />
                            <asp:Parameter Name="email" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="nume" Type="String" />
                            <asp:Parameter Name="prenume" Type="String" />
                            <asp:Parameter Name="username" Type="String" />
                            <asp:Parameter Name="parola" Type="String" />
                            <asp:Parameter DbType="Date" Name="data_nastere" />
                            <asp:Parameter Name="admin" Type="Int32" />
                            <asp:Parameter Name="email" Type="String" />
                            <asp:Parameter Name="id" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                   
                </div>
            </div>
            <div class="options">
                <div class="options_title">Utilizatori</div>
                <div class="options_filtru">
                    &nbsp;
                </div>
            </div>
</asp:Content>
