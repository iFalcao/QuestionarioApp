﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppQuestionario._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1 class="text-center">QuestApp</h1>
    </div>
         <div class="form-horizontal col-md-12"">
             <h4>Criar Questionário.</h4>
            <hr />
            <asp:ValidationSummary runat="server" CssClass="text-danger" />
             <asp:Label ID="lblError" runat="server" Text="" CssClass="text-danger"></asp:Label>
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
    <asp:GridView ID="tabelaQuestionarios" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="tabelaQuestionarios_RowCommand">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Id">
                <ItemTemplate>
                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nome">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("nome") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Link">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("link") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tipo">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("tipo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnExcluir" runat="server" Text="Deletar Questionário" CssClass="btn btn-danger" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Excluir"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
        

</asp:Content>
