<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="AgregarDinamica.aspx.cs" Inherits="UserInterface.Operacion.MantenimientoDinamica.AgregarDinamica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .formulario
        {
        }
        .columna1
        {
            width: 150px;
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
                <span class="titulo">Agregar Paso Dinámica</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table style="margin: auto;">
        <tr>
            <td>
                <table class="style1">
                    <tr>
                        <td class="columna1">
                            <span class="texto">Grupo Mensaje</span>
                        </td>
                        <td>
                            <asp:Label ID="lblNombreGrupo" runat="server" CssClass="texto"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="columna1">
                            <span class="texto">Mensaje</span>
                        </td>
                        <td>
                            <asp:Label ID="lblNombreMensaje" runat="server" CssClass="texto"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="columna1">
                            <span class="texto">Mensaje Transaccional</span>
                        </td>
                        <td>
                            <asp:Label ID="lblMensajeTransaccional" runat="server" CssClass="texto"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="columna1">
                            <span class="texto">Número</span>
                        </td>
                        <td>
                            <asp:Label ID="lblNumero" runat="server" CssClass="texto"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="columna1">
                            <span class="texto">Tipo Funcionalidad</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlTipoFuncionalidad" runat="server" AutoPostBack="True" DataSourceID="dsTipoFuncionalidad"
                                DataTextField="Value" DataValueField="Key" AppendDataBoundItems="True" EnableViewState="False"
                                Width="200px">
                                <asp:ListItem Value="-1">Seleccionar Tipo Funcionalidad</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="dsTipoFuncionalidad" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerTipoFuncionalidad" TypeName="BusinessLayer.Operacion.TipoFuncionalidadBL">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <%if (this.drlTipoFuncionalidad.SelectedValue == BusinessLayer.Operacion.TipoFuncionalidadBL.obtenerCodigoEnviar().ToString() ||
                              this.drlTipoFuncionalidad.SelectedValue == BusinessLayer.Operacion.TipoFuncionalidadBL.obtenerCodigoRecibir().ToString())
                          {%>
                        <div id="divComunicacion" runat="server">
                            <table class="style1">
                                <tr>
                                    <td class="columna1">
                                        <span class="texto">Entidad Comunicación</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drlEntidad" runat="server" DataSourceID="dsEntidadComunicacion"
                                            DataTextField="EDC_NOMBRE" DataValueField="EDC_CODIGO"
                                            AppendDataBoundItems="True" EnableViewState="False" Width="200px">
                                            <asp:ListItem Value="-1">Seleccionar Entidad Comunicación</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="dsEntidadComunicacion" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="obtenerEntidadComunicacionSinRelaciones" TypeName="BusinessLayer.Comunicacion.EntidadComunicacionBL">
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="columna1">
                                        <span class="texto">Reintentos</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReintentos" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <% } %>
                        <%if (this.drlTipoFuncionalidad.SelectedValue == BusinessLayer.Operacion.TipoFuncionalidadBL.obtenerCodigoCriptografia().ToString())
                          {%>
                        <div id="div1" runat="server">
                            <table class="style1">
                                <tr>
                                    <td class="columna1">
                                        <span class="texto">Criptografía</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drlCriptografia" runat="server" DataSourceID="dsCriptografia"
                                            DataTextField="DNC_NOMBRE" DataValueField="DNC_CODIGO"
                                            AppendDataBoundItems="True" EnableViewState="False" Width="200px">
                                            <asp:ListItem Value="-1">Seleccionar Criptografía</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="dsCriptografia" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="obtenerDinamicaCriptografiaPorMensaje" TypeName="BusinessLayer.Seguridad.DinamicaCriptografiaBL">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="-1" Name="codigoMensaje" SessionField="codigoMensaje"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <% } %>
                        <%if (this.drlTipoFuncionalidad.SelectedValue == BusinessLayer.Operacion.TipoFuncionalidadBL.obtenerCodigoTransformacion().ToString())
                          {%>
                        <div id="div2" runat="server">
                            <table class="style1">
                                <tr>
                                    <td class="columna1">
                                        <span class="texto">Transformacíon</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drlTransformacion" runat="server" DataSourceID="dsTransformacion"
                                            DataTextField="TRM_NOMBRE" DataValueField="TRM_CODIGO"
                                            AppendDataBoundItems="True" EnableViewState="False" Width="200px">
                                            <asp:ListItem Value="-1">Seleccionar Transformación</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="dsTransformacion" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="obtenerTransformacionSinRelacionesPorMensajeOrigen" TypeName="BusinessLayer.Operacion.TransformacionMensajeBL">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="-1" Name="codigoMensajeOrigen" SessionField="codigoMensaje"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <% } %>
                        <%if (this.drlTipoFuncionalidad.SelectedValue == BusinessLayer.Operacion.TipoFuncionalidadBL.obtenerCodigoValidacion().ToString())
                          {%>
                        <div id="div3" runat="server">
                            <table class="style1">
                                <tr>
                                    <td class="columna1">
                                        <span class="texto">Validación</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drlValidacion" runat="server" DataSourceID="dsValidacion" DataTextField="GRV_NOMBRE"
                                            DataValueField="GRV_CODIGO"
                                            AppendDataBoundItems="True" EnableViewState="False" Width="200px">
                                            <asp:ListItem Value="-1">Seleccionar Validación</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="dsValidacion" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="obtenerGrupoValidacionSinRelaciones" TypeName="BusinessLayer.Mensajeria.GrupoValidacionBL">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="-1" Name="mensaje" SessionField="codigoMensaje"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <% } %>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drlTipoFuncionalidad" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <table class="style1">
                    <tr>
                        <td style="text-align: center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" 
                                onclick="btnAceptar_Click" />
                            <asp:Button ID="btnRegresar" runat="server" Text="Cancelar" 
                                onclick="btnRegresar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Se han producido los siguientes errores:"
                    ValidationGroup="Grupo1" ShowMessageBox="True" ShowSummary="False" />
            </td>
        </tr>
    </table>
</asp:Content>
