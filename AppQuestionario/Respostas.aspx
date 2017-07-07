﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="Respostas.aspx.cs" Inherits="AppQuestionario.Respostas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="lblX" runat="server" Text="Selecione a pergunta"></asp:Label>
    <asp:DropDownList ID="ddlPerguntas" runat="server"></asp:DropDownList><br />
    <br />
    <asp:Button ID="btnListarRespostas" runat="server" Text="Listar Respostas" CssClass="btn btn-default" OnClick="btnListarRespostas_Click"/>
    <hr />
    <div class="row">
        <div class="col-md-6">
        <asp:GridView ID="tabelaRespostas" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="tabelaRespostas_RowCommand" >
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
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("correta").Equals(Convert.ToChar("S")) ? "Sim" : "Não" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ordem">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("ordem") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnExcluir" runat="server" Text="Deletar Resposta" CssClass="btn btn-danger" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Excluir"/>
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
        </div>
        <div class="col-md-6">
        <div class="form-horizontal col-md-12">
            <h4>Criar Resposta.</h4>
            <hr />
            <asp:ValidationSummary runat="server" CssClass="text-danger" />
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-6">Id da Pergunta</asp:Label>
                    <div class="col-md-6">
                        <asp:Label ID="lblIdPergunta" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtDescricao" CssClass="col-md-6">Descrição da Resposta</asp:Label>
                    <div class="col-md-6">
                        <!-- Create a validation group to allow two different 'forms' -->
                        <asp:TextBox runat="server" ID="txtDescricao" CssClass="form-control" ValidationGroup="Two"/>
                        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="Two" ControlToValidate="txtDescricao"
                            CssClass="text-danger" ErrorMessage="A descrição é obrigatória" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-6">Essa é a resposta correta?</asp:Label>
                    <div class="col-md-6">
                        <asp:CheckBox ID="chkCorreta" runat="server" />&nbsp;&nbsp;&nbsp;Sim
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtOrdem" CssClass="col-md-6">Ordem da resposta</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtOrdem" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" Display="Dynamic" ValidationGroup="Two" ControlToValidate="txtOrdem"
                            CssClass="text-danger" ErrorMessage="A ordem é obrigatória" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-10">
                        <asp:Button ID="btnCriar" runat="server" Text="Criar Resposta" CssClass="btn btn-success" OnClick="btnCriar_Click" />
                    </div>
                </div>
            </div>
      </div>
    </div>
</asp:Content>