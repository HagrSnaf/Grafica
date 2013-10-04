<%@ Page Title="" Language="C#" MasterPageFile="~/View.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebApplication1.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    function urla() {
        alert($("#ContentPlaceHolder1_Label1").text());
    }
</script>
<style>
         .radios .Redbutton label
        {
            float:left;
        }
         .radios .Redbutton input
        {
            float:left;
        }
        .radios .Redbutton input{border:none; background-color:red;width:23px;height:23px;}
        
         .radios .Greenbutton
        {
            color:green;
        }
        .radios .Greenbutton input{border:none; background-color:green;}
    </style>

      
    <asp:Panel ID="continut_test" runat="server">
</asp:Panel>
<asp:Label ID="Label1" runat="server" Text="" style="color:Red"></asp:Label>  
</asp:Content>
