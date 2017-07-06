<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Perguntas.aspx.cs" Inherits="AppQuestionario.Perguntas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Selecione o questionário"></asp:Label>
    <asp:DropDownList ID="ddlQuestionarios" runat="server" OnSelectedIndexChanged="ddlQuestionarios_SelectedIndexChanged"></asp:DropDownList>

    
    

</asp:Content>

