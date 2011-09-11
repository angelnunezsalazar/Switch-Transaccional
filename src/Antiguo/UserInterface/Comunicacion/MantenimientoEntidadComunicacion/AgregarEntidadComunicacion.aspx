<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="AgregarEntidadComunicacion.aspx.cs" Inherits="UserInterface.Comunicacion.MantenimientoEntidadComunicacion.AgregarEntidadComunicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 140px;
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
                <span class="titulo">Agregar Entidad Comunicaci&oacute;n</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" align="center" width="550px">
                    <tr>
                        <td class="style2 form_columna1">
                            <span class="texto1">Nombre</span>
                        </td>
                        <td class="form_columna2">
                            <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Width="220px" CssClass="control" />
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                                ErrorMessage="Debe ingresar el Nombre" ValidationGroup="AgregarEntidad">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="form_columna3"></td>
                    </tr>
                    <tr>
                        <td class="style2 form_columna1">
                            <span class="texto1">Descripci&oacute;n</span>
                        </td>
                        <td class="form_columna2">
                            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="5" Width="274px" CssClass="control"
                                MaxLength="200" />
                        </td>
                        <td class="form_columna3"></td>
                    </tr>
                    <tr>
                        <td class="style2 form_columna1">
                            <span class="texto1">Nombre Cola</span>
                            
                            </td>
                        <td class="form_columna2">
                            <asp:TextBox ID="txtCola" runat="server" MaxLength="50" Width="220px" CssClass="control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCola" runat="server" ControlToValidate="txtCola"
                                ErrorMessage="Debe ingresar el Nombre Cola" ValidationGroup="AgregarEntidad">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="form_columna3"></td>
                    </tr>
                    <tr>
                        <td class="style2 form_columna1">
                            <span class="texto1">Ruta Log</span>
                        </td>
                        <td class="form_columna2">
                            <asp:TextBox ID="txtDirectorioLog" runat="server" Style="width: 350px;" MaxLength="300" CssClass="control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRutaLog" runat="server" ControlToValidate="txtDirectorioLog"
                                ErrorMessage="Debe ingresar Ruta Log" ValidationGroup="AgregarEntidad">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="form_columna3"></td>
                    </tr>
                    <tr>
                        <td class="style2 form_columna1">
                            <span class="texto1">Nombre Log</span>
                        </td>
                        <td class="form_columna2">
                            <asp:TextBox ID="txtNombreLog" runat="server" MaxLength="50" Width="220px" CssClass="control" />
                            <asp:RequiredFieldValidator ID="rfvNombreLog" runat="server" ControlToValidate="txtNombreLog"
                                ErrorMessage="Debe ingresar Nombre Log" ValidationGroup="AgregarEntidad">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="form_columna3"></td>
                    </tr>
                    <tr>
                        <td class="style2 form_columna1">
                            <span class="texto1">TimeOut Cola (seg)</span>
                        </td>
                        <td class="form_columna2">
                            <asp:TextBox ID="txtTimeOutCola" runat="server" MaxLength="4" Width="65px" CssClass="control" />
                            <asp:RequiredFieldValidator ID="rfvTimeOutCola" runat="server" ControlToValidate="txtTimeOutCola"
                                ErrorMessage="Debe ingresar TimeOut Cola" ValidationGroup="AgregarEntidad" Display="Dynamic">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTimeOutCola"
                                ErrorMessage="Debe ingresar un valor válido para el TimeOut Cola" ValidationExpression="\d*"
                                ValidationGroup="AgregarEntidad">*</asp:RegularExpressionValidator>
                        </td>
                        <td class="form_columna3"></td>
                    </tr>
                    <tr>
                        <td class="style2 form_columna1">
                            <span class="texto1">Protocolo</span>
                        </td>
                        <td class="form_columna2">
                            <asp:DropDownList ID="drlProtocolo" runat="server" DataSourceID="dsProtocolo" DataTextField="PTR_NOMBRE" CssClass="control"
                                DataValueField="PTR_CODIGO" AppendDataBoundItems="True">
                                <asp:ListItem Value="-1">Seleccionar Protocolo</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvProtocolo" runat="server" ControlToValidate="drlProtocolo"
                                ErrorMessage="Debe seleccionar el Protocolo" InitialValue="-1" 
                                ValidationGroup="AgregarEntidad">*</asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="dsProtocolo" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerProtocolosNoAsignados" TypeName="BusinessLayer.Comunicacion.ProtocoloBL">
                            </asp:ObjectDataSource>
                        </td>
                        <td class="form_columna3"></td>
                    </tr>
                    <tr>
                        <td class="style2 form_columna1">
                            <span class="texto1">Tipo Entidad</span>
                        </td>
                        <td class="form_columna2">
                            <asp:DropDownList ID="drlTipoEntidad" runat="server" 
                                AppendDataBoundItems="True" DataSourceID="dsTipoEntidad" 
                                DataTextField="TEM_NOMBRE" DataValueField="TEM_CODIGO" CssClass="control">
                                <asp:ListItem Value="-1">Seleccionar Tipo Entidad</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="drlTipoEntidad" 
                                ErrorMessage="Debe seleccionar el Tipo Entidad" InitialValue="-1" 
                                ValidationGroup="AgregarEntidad">*</asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="dsTipoEntidad" runat="server" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectMethod="obtenerTipoTerminal" 
                                TypeName="BusinessLayer.Comunicacion.TipoEntidadBL"></asp:ObjectDataSource>
                        </td>
                        <td class="form_columna3"></td>
                    </tr>
                    <tr>
                        <td colspan="2" class="form_pie"></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="Button1" runat="server" CommandName="Insert" Text="Aceptar" OnClick="Button1_Click"
                                ValidationGroup="AgregarEntidad" />
                            <asp:Button ID="Button2" runat="server" PostBackUrl="~/Comunicacion/MantenimientoEntidadComunicacion/ConsultarEntidadComunicacion.aspx"
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
