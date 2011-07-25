<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true" CodeBehind="ModificarGrupoValidacion.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoGrupoValidacion.ModificarGrupoValidacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table align="center" style="width: 400px;">
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td align="center"><span class="titulo">Modificar Grupo de Validaci&oacute;n</span></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <table align="center" style="width: 400px;">
                            <tr>
                    <td>
                        <span class="texto">Grupo de Mensajes</span>
                    </td>
                    <td>
                        <asp:Label ID="lblGrupoMensaje" CssClass="texto" runat="server" />
                        <asp:HiddenField ID="hdnGrupoMensaje" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="texto">Mensaje</span>
                    </td>
                    <td>
                        <asp:Label ID="lblMensaje" CssClass="texto" runat="server" />
                        <asp:HiddenField ID="hdnMensaje" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px;">
                        <span class="texto">C&oacute;digo</span>
                    </td>
                    <td>
                        <asp:Label ID="lblCodigo" CssClass="texto" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="texto">Nombre</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombre" runat="server" Width="200"/>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" 
                onclick="btnAceptar_Click" />
            &nbsp;<asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                              PostBackUrl="~/Mensajeria/MantenimientoGrupoValidacion/ConsultarGrupoValidacion.aspx" />
        </td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnDefinirReglas" runat="server" Text="Definir Reglas"
                        onclick="btnDefinirReglas_Click"  />
        </td>
    </tr>
</table>
</asp:Content>
