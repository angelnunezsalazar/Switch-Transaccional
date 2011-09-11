<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ModificarEntidadComunicacion.aspx.cs" Inherits="UserInterface.Comunicacion.MantenimientoEntidadComunicacion.ModificarEntidadComunicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 98px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="style1">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <span class="titulo">Modificar Entidad Comunicaci&oacute;n</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="500px" style="margin: auto;" class="tabla_mantenimiento">
                    <tr>
                        <td class="style2">
                            <span class="texto">Nombre</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Width="220px" />
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                                ErrorMessage="Ingrese el Nombre" ValidationGroup="AgregarEntidad">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <span class="texto">Descripci&oacute;n</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="5" Width="274px"
                                MaxLength="200" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <span class="texto">Nombre Cola</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCola" runat="server" MaxLength="50" Width="220px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCola" runat="server" ControlToValidate="txtCola"
                                ErrorMessage="Ingrese el Nombre Cola" ValidationGroup="AgregarEntidad">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <span class="texto">Ruta Log</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDirectorioLog" runat="server" Style="width: 350px;" MaxLength="300"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRutaLog" runat="server" ControlToValidate="txtDirectorioLog"
                                ErrorMessage="Ingrese la Ruta del Log" ValidationGroup="AgregarEntidad">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <span class="texto">Nombre Log</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombreLog" runat="server" MaxLength="300" Width="220px" />
                            <asp:RequiredFieldValidator ID="rfvNombreLog" runat="server" ControlToValidate="txtNombreLog"
                                ErrorMessage="Ingrese el Nombre Log" ValidationGroup="AgregarEntidad">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <span class="texto">TimeOut Cola (seg)</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTimeOutCola" runat="server" MaxLength="5" Width="65px" />
                            <asp:RequiredFieldValidator ID="rfvTimeOutCola" runat="server" ControlToValidate="txtTimeOutCola"
                                ErrorMessage="Ingrese el TimeOut Cola" ValidationGroup="AgregarEntidad">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTimeOutCola"
                                ErrorMessage="Debe ingresar un valor válido para el TimeOut Cola" ValidationExpression="\d*"
                                ValidationGroup="AgregarEntidad">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <span class="texto">Protocolo</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlProtocolo" runat="server" DataSourceID="dsProtocolo" DataTextField="PTR_NOMBRE"
                                DataValueField="PTR_CODIGO" AppendDataBoundItems="True">
                                <asp:ListItem Value="-1">Seleccionar Protocolo</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvProtocolo" runat="server" ControlToValidate="drlProtocolo"
                                ErrorMessage="Ingrese el Protocolo" InitialValue="-1" ValidationGroup="AgregarEntidad">*</asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="dsProtocolo" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerProtocolo" TypeName="BusinessLayer.Comunicacion.ProtocoloBL">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <span class="texto">Tipo Entidad</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlTipoEntidad" runat="server" AppendDataBoundItems="True"
                                DataSourceID="dsTipoEntidad" DataTextField="TEM_NOMBRE" DataValueField="TEM_CODIGO">
                                <asp:ListItem Value="-1">Seleccionar Tipo Entidad</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drlTipoEntidad"
                                ErrorMessage="Debe seleccionar el Tipo Entidad" InitialValue="-1" ValidationGroup="AgregarEntidad">*</asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="dsTipoEntidad" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerTipoTerminal" TypeName="BusinessLayer.Comunicacion.TipoEntidadBL">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnAceptar" runat="server" CommandName="Insert" Text="Aceptar" OnClick="Button1_Click"
                                ValidationGroup="AgregarEntidad" />
                            <asp:Button ID="btnCancelar" runat="server" PostBackUrl="~/Comunicacion/MantenimientoEntidadComunicacion/ConsultarEntidadComunicacion.aspx"
                                Text="Cancelar" />
                        </td>
                    </tr>
                </table>
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
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Se han producido los siguientes errores:"
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="AgregarEntidad" />
</asp:Content>
