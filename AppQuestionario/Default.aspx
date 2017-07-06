<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppQuestionario._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Criar Questionário ou Avaliação</h1>
        <asp:Label ID="Label2" runat="server" Text="Nome do Questionário"></asp:Label>
        <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
        <hr />
        <asp:Label ID="Label1" runat="server" Text="Tipo do Questionário"></asp:Label>
        <asp:DropDownList ID="ddlTipo" runat="server"></asp:DropDownList>
        <hr />
        <asp:Label ID="Label3" runat="server" Text="Link com instruções"></asp:Label>
        <asp:TextBox ID="txtLink" runat="server"></asp:TextBox>
        <hr />
        <asp:Button ID="btnCriar" runat="server" Text="Button" OnClick="btnCriar_Click" />
    </div>

</asp:Content>
