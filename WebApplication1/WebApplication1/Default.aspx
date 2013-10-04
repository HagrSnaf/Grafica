<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <h1 class="borderBottomH1">OpenGL</h1>
    <p>
        OpenGL (Open Graphics Library) este o specificație standard care definește o aplicatie cross-platform API (application programming interface) foarte utilizat pentru programarea componentelor grafice 2D si 3D ale programelor de calculator. Interfața constă in peste 250 de apeluri diferite care pot fi folosite pentru a desenta scene 3D complexe din primitive simple. OpenGL a fost dezvoltat de Silicon Graphics Inc. (SGI) in 1992 și este foarte utilizat in softuri CAD, realitate virtuală, visualizare științifică, simulări de zboruri sau jocuri pe calculator. Acest ultim domeniu este in strânsă competiție cu tehnologia DirectX de la Microsoft (vezi OpenGL vs. Direct3D). OpenGL este condus de un consortiu tehnologic non-profit, Khronos Grup.
    </p>
    <h1 class="borderBottomH1">
    Design
    </h1>
    <p>OpenGL servește două scopuri principale:</p>
    <ul class="lista">
        <li>Pentru a ascunde complexitatea interfațare cu diferite 3D acceleratoarele, prin prezentarea programatorului cu un singur API uniform.</li>
        <li>Pentru a ascunde capabilități diferitelor platforme hardware, prin solicitarea ca toate implementarile acceptă caracteristica OpenGL ca un set complet (cu ajutorul software-ul emulation, dacă este necesar).</li>
    </ul>
<p>Funcționarea OpenGL de bază este de a accepta primitive, cum ar fi puncte, linii și poligoane, și de a le converti în pixeli. Acest lucru se face printr-o conducta grafică - (graphics pipeline), cunoscută sub numele de mașină de stare OpenGL. Cele mai multe comenzi OpenGL primitive, fie problema la conducta grafica, sau configura felul în care aceste procese de conducte de primitive. Înainte de introducerea OpenGL 2.0, fiecare etapă din conducta efectuat o funcție fixă și a fost configurabil numai în limite restranse. OpenGL 2.0 oferă mai multe etape, care sunt pe deplin programabile folosind GLSL.

OpenGL este API procedural de nivel mic, care necesită ca un programator să impună măsurile exacte necesare pentru a face o scena. Acest lucru contrastează cu alte API-uri, în care un programator are nevoie doar pentru a descrie o scenă și poate lăsa biblioteca și gestiona detalile și redand finalul scenei. OpenGL's de nivel mic, impune programatori să aibă o bună cunoaștere a conducta grafică, dar, de asemenea, oferă o anumită libertatea de a pune în aplicare algoritmi noi de redare.
    </p>
</asp:Content>
