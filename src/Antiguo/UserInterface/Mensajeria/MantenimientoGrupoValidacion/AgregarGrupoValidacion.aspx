<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="AgregarGrupoValidacion.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoGrupoValidacion.AgregarGrupoValidacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center" style="width: 100%;">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <span class="titulo">Agregar Grupo de Validaci&oacute;n</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" align="center" style="width: 400px;">
                    <tr>
                        <td style="width: 150px;" class="form_columna1">
                            <span class="texto1">Nombre</span>
                        </td>
                        <td class="form_columna2">
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="control"  Width="200"/>
                        </td>
                        <td class="form_columna3">
                        </td>
                    </tr>
                </table>
                <asp:FormView ID="frmGrupoMensaje" runat="server" DataSourceID="dsGrupoMensaje" CellPadding="0"
                    HorizontalAlign="Center">
                    <ItemTemplate>
                        <table border="0" cellspacing="0" cellpadding="0" align="center" style="width: 400px;">
                            <tr>
                                <td style="width: 150px;" class="form_columna1">
                                    <span class="texto1">Grupo de Mensajes</span>
                                </td>
                                <td class="form_columna2">
                                    <asp:Label ID="lblNombreGrupoMensaje" CssClass="texto" runat="server" Text='<%# Bind("GMJ_NOMBRE") %>' />
                                    <asp:HiddenField ID="hdnCodigoGrupoMensaje" Value='<%# Bind("GMJ_CODIGO") %>' runat="server" />
                                    <asp:HiddenField ID="hdnTipoMensajeCodigo" runat="server" Value='<%# Bind("TIPO_MENSAJE.TMJ_CODIGO") %>' />
                                </td>
                                <td class="form_columna3">
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="dsGrupoMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerGrupoMensaje" TypeName="BusinessLayer.Mensajeria.GrupoMensajeBL">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="codigo" QueryStringField="grupo" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:FormView ID="frmMensaje" runat="server" DataSourceID="dsMensaje" CellPadding="0"
                    HorizontalAlign="Center">
                    <ItemTemplate>
                        <table border="0" cellspacing="0" cellpadding="0" align="center" style="width: 400px;">
                            <tr>
                                <td style="width: 150px;" class="form_columna1">
                                    <span class="texto1">Mensaje</span>
                                </td>
                                <td class="form_columna2">
                                    <asp:Label ID="lblNombreMensaje" CssClass="texto" runat="server" Text='<%# Bind("MEN_NOMBRE") %>' />
                                    <asp:HiddenField ID="hdnCodigoMensaje" Value='<%# Bind("MEN_CODIGO") %>' runat="server" />
                                </td>
                                <td class="form_columna3">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="form_pie">
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="dsMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerMensaje" TypeName="BusinessLayer.Mensajeria.MensajeBL">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="codigo" QueryStringField="mensaje" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <table border="0" cellspacing="0" cellpadding="0" align="center" style="width: 400px;">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" PostBackUrl="~/Mensajeria/MantenimientoGrupoValidacion/ConsultarGrupoValidacion.aspx" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
