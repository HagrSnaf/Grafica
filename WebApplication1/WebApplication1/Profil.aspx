<%@ Page Title="" Language="C#" MasterPageFile="~/View.Master" AutoEventWireup="true" CodeBehind="Profil.aspx.cs" Inherits="WebApplication1.Profil" %>
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
    width:20%;
    height:auto;
    margin:10px 0px 10px 10px;
    float:left;
    font-size:17px;
    overflow: auto;
    
}
.profil
{
    width:90%;
    
    height:540px;
    float:right;
    padding:5px;
    border-style:ridge;
    border:none;
    border-left:2px ridge #000;
    overflow: auto;
    text-align:center;
    font-family:'Garamond';
    font-size:17px;
    margin: 10px 0px 10px 10px;
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
    height:20px;
    width:70%;
}
.tit a{
    color:black;
    text-decoration:none;
}
.tit a:hover{
    color:black;
    text-decoration: underline;
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
    height: 570px;
    overflow :auto;
}


    
</style>

<asp:Panel ID="box_panel" CssClass="box" runat="server">


             <asp:Panel  ID="recomandari" runat="server"  CssClass="profil" >
               
            </asp:Panel>

            <div class="clear"></div>
 </asp:Panel>


 </asp:Content>

