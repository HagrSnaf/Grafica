<%@ Page Title="" Language="C#" MasterPageFile="~/View.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebApplication1.View1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">
    function desfasoara(id) {
        $("#ContentPlaceHolder1_lect" + id).toggle();
    }
</script>


        <style>
    
    .capitole
{
    width:23.6%;
    height:540px;
    margin:10px 0px 10px 10px;
    float:left;
    font-size:17px;
    overflow: auto;
    
}
.lectie
{
    width:74%;
    margin:10px 0px 10px 0px;
    height:540px;
    float:right;
    padding:5px;
    border-style:solid;
    border:none;
    border-left:2px solid #000;
    overflow: auto;
}

.lect
{
    margin-top: 3px;
    margin-left:3px;
    padding-left:13px;
    font-size:12px;

    border-style:solid;
    border:none;
    border-left:2px solid #B11116;
}

.tes
{
    margin-left:23px;
    font-size:12px;
    }

.tit
{
    cursor:pointer;
    margin-top:5px;
    font-size:15px;
    height:auto;
    width:100%;
 
}
.tit a{
    text-decoration:none;
       -moz-border-radius: 7px;
    border-radius: 7px;
    padding: 0px 3px 0px 3px;
}
.tit a:hover{
    text-decoration: underline;
}
.ancora
{
    text-decoration:none;
       -moz-border-radius: 7px;
    border-radius: 7px;
    padding: 0px 3px 0px 3px;
}
.lect a{
    color:black;
    text-decoration:none;
}
.lect a:hover{
    color:black;
    text-decoration: underline;
}
#rosu
{
    background-color:Red;
}
.box
{
    background-color: #EEEEEE;
    border-radius: 8px 8px 8px 8px;
    box-shadow: 0 3px 5px #171717;
    height:570px;
}


    
</style>

<asp:Panel ID="box_panel" CssClass="box" runat="server">

            <asp:Panel ID="lista_cap" runat="server"   CssClass="capitole" >
            </asp:Panel>

            <asp:Panel  ID="continut_lect" runat="server"  CssClass="lectie" >
               
            </asp:Panel>
            <div class="clear"></div>
 </asp:Panel>


 </asp:Content>
