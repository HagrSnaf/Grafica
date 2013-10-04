<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Galerie.aspx.cs" Inherits="WebApplication1.Galerie" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="borderBottomH1">Galerie Foto</h1>
    <div id="lb">  
     <table border="0" class="centeredTable">
     <tr>
        <td><p><a title='titlu'  rel="lightbox[Galerie]"  href='galerie/poza1.jpg'><img src="galerie/poza1.jpg" alt="poza1" title="poza1" width="330" class="pictLeft" /></a></p></td>
        <td><p><a title='titlu'  rel="lightbox[Galerie]"  href='galerie/poza2.jpg'><img src="galerie/poza2.jpg" alt="poza2" title="poza2" width="330" class="pictRight" /></a></p></td>
    </tr>
    <tr>
        <td><p><a title='titlu'  rel="lightbox[Galerie]"  href='galerie/poza3.jpg'><img src="galerie/poza3.jpg" alt="poza3" title="poza3" width="330" class="pictLeft" /></a></p></td>
        <td><p><a title='titlu'  rel="lightbox[Galerie]"  href='galerie/poza4.jpg'><img src="galerie/poza4.jpg" alt="poza4" title="poza4" width="330" class="pictRight" /></a></p></td>        
    </tr>
    <tr>
        <td><p><a title='titlu'  rel="lightbox[Galerie]"  href='galerie/poza5.jpg'><img src="galerie/poza5.jpg" alt="poza5" title="poza5" width="330" class="pictLeft" /></a></p></td>
        <td><p><a title='titlu'  rel="lightbox[Galerie]"  href='galerie/poza6.jpg'><img src="galerie/poza6.jpg" alt="poza6" title="poza6" width="330" class="pictRight" /></a></p></td>
    </tr>
    </table>
</div>

</asp:Content>
