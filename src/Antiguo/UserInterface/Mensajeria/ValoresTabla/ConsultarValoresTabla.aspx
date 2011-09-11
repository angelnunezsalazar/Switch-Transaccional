<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true" CodeBehind="ConsultarValoresTabla.aspx.cs" Inherits="UserInterface.Mensajeria.ValoresTabla.ConsultarValoresTabla" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="style1">
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <span class="titulo">Consultar Valores Tabla </span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table align="center" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <span class="texto">Tabla</span>
            </td>
            <td>
                <asp:DropDownList ID="drlTabla" runat="server" DataSourceID="dsTabla"
                    DataTextField="TBL_NOMBRE" DataValueField="TBL_CODIGO" AppendDataBoundItems="True"
                    AutoPostBack="True" 
                    OnSelectedIndexChanged="drlGrupoMensaje_SelectedIndexChanged">
                    <asp:ListItem Value="-1">Seleccionar Tabla</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="dsTabla" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerTabla" TypeName="BusinessLayer.Mensajeria.TablaBL"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                <asp:GridView ID="grvValoresTabla" runat="server"
                AutoGenerateColumns="False" ShowFooter="true"
                    onrowcancelingedit="grvValoresTabla_RowCancelingEdit" 
                    onrowediting="grvValoresTabla_RowEditing" 
                    onrowcommand="grvValoresTabla_RowCommand" 
                    onrowdeleting="grvValoresTabla_RowDeleting" 
                    onrowupdating="grvValoresTabla_RowUpdating" >
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>