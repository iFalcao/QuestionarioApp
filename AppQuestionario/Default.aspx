<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppQuestionario._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>QuestApp</h1>
    </div>
         <div class="form-horizontal col-md-12"">
             <h4>Criar Questionário.</h4>
            <hr />
            <asp:ValidationSummary runat="server" CssClass="text-danger" />
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Nome" CssClass="col-md-2">Nome do Questionário</asp:Label>
                <div class="col-md-10">
                    <!-- Create a validation group to allow two different 'forms' -->
                    <asp:TextBox runat="server" ID="Nome" CssClass="form-control" ValidationGroup="Two"/>
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="Two" ControlToValidate="Nome"
                        CssClass="text-danger" ErrorMessage="O nome é obrigatório" />
                </div>
            </div>
             <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Link" CssClass="col-md-2">Link com instruções</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Link" CssClass="form-control" ValidationGroup="Two"/>
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="Two" ControlToValidate="Link"
                        CssClass="text-danger" ErrorMessage="O link é obrigatório" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlTipos" CssClass="col-md-2">Tipo de Questionário</asp:Label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlTipos" runat="server" CssClass="form-control" ValidationGroup="Two"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="Two" ControlToValidate="ddlTipos"
                        CssClass="text-danger" ErrorMessage="O tipo é obrigatório" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-10">
                    <asp:Button ID="btnCriar" runat="server" Text="Criar Questionário" OnClick="btnCriar_Click"  CssClass="btn btn-success" />
                </div>
            </div>
        </div>
</asp:Content>
