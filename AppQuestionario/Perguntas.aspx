﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Perguntas.aspx.cs" Inherits="AppQuestionario.Perguntas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" Text="Selecione o questionário"></asp:Label>
    <asp:DropDownList ID="ddlQuestionarios" runat="server"></asp:DropDownList><br />
    <br />
    <asp:Button ID="btnListarPerguntas" runat="server" Text="Listar Perguntas" CssClass="btn btn-default" OnClick="btnListarPerguntas_Click"/>
    <hr />
    <asp:GridView ID="tabelaPerguntas" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Id">
                <ItemTemplate>
                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Descricao">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("descricao") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Obrigatória">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("obrigatoria") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tipo">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("tipo").Equals('U') ? "Única Escolha" : "Múltipla Escolha" %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ordem">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("ordem") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnExcluir" runat="server" Text="Deletar Pergunta" CssClass="btn btn-danger" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Excluir"/>
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
    <hr />
    <div class="form-horizontal col-md-12">
             <h4>Criar Pergunta.</h4>
            <hr />
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlTipos" CssClass="col-md-2">Id do Questionário</asp:Label>
                <div class="col-md-4">
                    <asp:Label ID="lblIdQuestionario" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <asp:ValidationSummary runat="server" CssClass="text-danger" />
             <asp:Label ID="lblError" runat="server" Text="" CssClass="text-danger"></asp:Label>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Descricao" CssClass="col-md-2">Descrição da Pergunta</asp:Label>
                <div class="col-md-10">
                    <!-- Create a validation group to allow two different 'forms' -->
                    <asp:TextBox runat="server" ID="txtDescricao" CssClass="form-control" ValidationGroup="Two"/>
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="Two" ControlToValidate="Descricao"
                        CssClass="text-danger" ErrorMessage="A descrição é obrigatória" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlTipos" CssClass="col-md-2">Tipo de Pergunta</asp:Label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlTipos" runat="server" CssClass="form-control" ValidationGroup="Two"></asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="Two" ControlToValidate="ddlTipos"
                        CssClass="text-danger" ErrorMessage="O tipo é obrigatório" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" CssClass="col-md-2">A pergunta é obrigatória?</asp:Label>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkObrigatoria" runat="server" />&nbsp;&nbsp;&nbsp;Sim
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlTipos" CssClass="col-md-2">Ordem da pergunta</asp:Label>
                <div class="col-md-4">
                    <asp:TextBox ID="txtOrdem" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="Two" ControlToValidate="txtOrdem"
                        CssClass="text-danger" ErrorMessage="A ordem é obrigatória" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-10">
                    <asp:Button ID="btnCriar" runat="server" Text="Criar Pergunta" CssClass="btn btn-success" OnClick="btnCriar_Click" />
                </div>
            </div>
        </div>

</asp:Content>

