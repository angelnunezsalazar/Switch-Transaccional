<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ModificarTransformacionCampo.aspx.cs" Inherits="UserInterface.Operacion.MantenimientoTransformacionCampo.ModificarTransformacionCampo" %>

<%@ Import Namespace="BusinessLayer.Operacion" %>
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
            width: 170px;
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
                <span class="titulo">Modificar Transformación Campo</span>
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
                <asp:FormView ID="frmTransformacion" runat="server" DataSourceID="dsTransformacion">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Transformacion</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblNombre" CssClass="texto" runat="server" Text='<%# Bind("TRM_NOMBRE") %>' />
                                    <asp:HiddenField ID="hndCodigoMensajeOrigen" runat="server" Value='<%# Bind("TRM_CODIGO") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Grupo Mensaje Origen</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblGrupoOrigen" CssClass="texto" runat="server" Text='<%# Bind("MENSAJE_ORIGEN.GRUPO_MENSAJE.GMJ_NOMBRE") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Mensaje Origen</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblMensajeOrigen" CssClass="texto" runat="server" Text='<%# Bind("MENSAJE_ORIGEN.MEN_NOMBRE") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Grupo Mensaje Destino</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblGrupoFin" CssClass="texto" runat="server" Text='<%# Bind("MENSAJE_DESTINO.GRUPO_MENSAJE.GMJ_NOMBRE") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Mensaje</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblMensajeDestino" CssClass="texto" runat="server" Text='<%# Bind("MENSAJE_DESTINO.MEN_NOMBRE") %>' />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="dsTransformacion" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerTransformacion" TypeName="BusinessLayer.Operacion.TransformacionMensajeBL">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoTransformacion" QueryStringField="Transformacion"
                            Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:FormView ID="frmCampodestino" runat="server" DataSourceID="dsCampo">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Campo Destino</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblNombre" CssClass="texto" runat="server" Text='<%# Bind("CAM_NOMBRE") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Tipo Dato</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblTipoDato" CssClass="texto" runat="server" Text='<%# Bind("TIPO_DATO.TDT_NOMBRE") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Longitud</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblLongitud" CssClass="texto" runat="server" Text='<%# Bind("CAM_LONGITUD") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Requerido</span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkRequerido" runat="server" Checked='<%# Bind("CAM_REQUERIDO") %>'
                                        Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="dsCampo" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerCampo" TypeName="BusinessLayer.Mensajeria.CampoMensajeBL">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoMensaje" QueryStringField="Mensaje"
                            Type="Int32" />
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoCampo" QueryStringField="Campo"
                            Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <table class="style1">
                    <tr>
                        <td class="columna1">
                            <span class="texto">Tipo Transformacion</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlTipoTransformacion" runat="server" DataSourceID="dsTipoTransformacion"
                                DataTextField="Value" DataValueField="Key" AppendDataBoundItems="True"
                                EnableViewState="False" AutoPostBack="True" Width="220px">
                                <asp:ListItem Value="-1">Seleccionar Tipo Transformacion</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvTipoTransformacion" runat="server" ControlToValidate="drlTipoTransformacion"
                                ErrorMessage="Debe ingresar el Tipo Transformacion" InitialValue="-1" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="dsTipoTransformacion" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerTipoTransformacion" TypeName="BusinessLayer.Operacion.TipoTransformacionBL">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
                <%if (this.drlTipoTransformacion.SelectedValue == TipoTransformacionBL.obtenerCodigoValorConstante().ToString())
                  {%>
                <div id="divValorConstante" runat="server">
                    <table class="style1">
                        <tr>
                            <td class="columna1">
                                <span class="texto">Valor Constante</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtValorConstante" Width="220" MaxLength="200" runat="server" Text='<%# Bind("TCM_VALOR_CONSTANTE") %>' />
                                <asp:RequiredFieldValidator ID="rfvConstante" runat="server" ControlToValidate="txtValorConstante"
                                    ErrorMessage="Debe ingresar el Valor Constante" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <% } %>
                <%if (this.drlTipoTransformacion.SelectedValue == TipoTransformacionBL.obtenerCodigoConcatenacion().ToString())
                  {%>
                <%if (ViewState["tipoTransformacion"].ToString() == TipoTransformacionBL.obtenerCodigoConcatenacion().ToString())
                  {%>
                <div id="divConcatenacion" runat="server">
                    <table class="style1">
                        <tr>
                            <td class="columna1">
                                <span class="texto">Parametros</span>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgParametrosConcatenacion" runat="server" ImageUrl="~/Includes/Imagenes/iconFind.png"
                                    OnClick="btnParametros_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <% } %>
                <% } %>
                <%if (this.drlTipoTransformacion.SelectedValue == TipoTransformacionBL.obtenerCodigoProcedimientoAlmacenado().ToString())
                  {%>
                <div id="divProcedimientoBD" runat="server">
                    <table class="style1">
                        <tr>
                            <td class="columna1">
                                <span class="texto">Procedimiento BD</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProcedimientoBD" Width="220" MaxLength="100" runat="server" Text='<%# Bind("TCM_PROCEDIMIENTO") %>' />
                                <asp:RequiredFieldValidator ID="rfvProcedimiento" runat="server" ControlToValidate="txtProcedimientoBD"
                                    ErrorMessage="Debe ingresar el Procedimiento BD" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <%if (ViewState["tipoTransformacion"].ToString() == TipoTransformacionBL.obtenerCodigoProcedimientoAlmacenado().ToString())
                          {%>
                        <tr>
                            <td class="columna1">
                                <span class="texto">Parametros</span>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnParametrosProcedimientoBD" runat="server" ImageUrl="~/Includes/Imagenes/iconFind.png"
                                    OnClick="btnParametros_Click" />
                            </td>
                        </tr>
                        <% } %>
                    </table>
                </div>
                <% } %>
                <%if (this.drlTipoTransformacion.SelectedValue == TipoTransformacionBL.obtenerCodigoFuncionalidadEstandar().ToString())
                  {%>
                <div id="divFuncionalidadEstandar" runat="server">
                    <table class="style1">
                        <tr>
                            <td class="columna1">
                                <span class="texto">Funcionalidad Estandar</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="drlFuncionalidadEstandar" runat="server" AppendDataBoundItems="True"
                                    DataSourceID="dsFuncionalidadEstandar" DataTextField="Value" 
                                    DataValueField="Key">
                                    <asp:ListItem Value="-1">Seleccionar Funcionalidad Estandar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvEstandar" runat="server" ControlToValidate="drlFuncionalidadEstandar"
                                    InitialValue="-1" ErrorMessage="Debe ingresar la Funcionalidad Estandar" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                <asp:ObjectDataSource ID="dsFuncionalidadEstandar" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="obtenerFuncionalidadEstandar" 
                                    TypeName="BusinessLayer.Operacion.FuncionalidadEstandarBL">
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </div>
                <% } %>
                <%if (this.drlTipoTransformacion.SelectedValue == TipoTransformacionBL.obtenerCodigoComponente().ToString())
                  {%>
                <div id="divComponente" runat="server">
                    <table class="style1">
                        <tr>
                            <td class="columna1">
                                <span class="texto">Componente</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="drlComponente" runat="server" AppendDataBoundItems="True" DataSourceID="dsComponente"
                                    DataTextField="COM_NOMBRE" DataValueField="COM_NOMBRE">
                                    <asp:ListItem Value="-1">Seleccionar Componente</asp:ListItem>
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="dsComponente" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="obtenerComponente" TypeName="BusinessLayer.Parametros.ComponenteBL">
                                </asp:ObjectDataSource>
                                <asp:RequiredFieldValidator ID="rfvComponente" runat="server" ControlToValidate="drlComponente"
                                    ErrorMessage="Debe ingresar el Componente" ValidationGroup="Grupo1" InitialValue="-1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="columna1">
                                <span class="texto">Nombre Clase</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtClase" Width="220" MaxLength="50" runat="server" Text='<%# Bind("TCM_CLASE") %>' />
                                <asp:RequiredFieldValidator ID="rfvClase" runat="server" ControlToValidate="txtClase"
                                    ErrorMessage="Debe ingresar el Nombre Clase" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revClase" runat="server" ControlToValidate="txtClase"
                                    ErrorMessage="Debe ingresar una Clase válida [a-b;A-z]" ValidationExpression="\w*"
                                    ValidationGroup="Grupo1">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="columna1">
                                <span class="texto">Nombre Metodo</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMetodo" Width="220" MaxLength="50" runat="server" Text='<%# Bind("TCM_METODO") %>' />
                                <asp:RequiredFieldValidator ID="rfvMetodo" runat="server" ControlToValidate="txtMetodo"
                                    ErrorMessage="Debe ingresar Nombre Método" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revMetodo" runat="server" ControlToValidate="txtMetodo"
                                    ErrorMessage="Debe ingresar una Método válido [a-b;A-z]" ValidationExpression="\w*"
                                    ValidationGroup="Grupo1">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <% } %>
                <table class="style1">
                    <tr>
                        <td style="text-align: center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" ValidationGroup="Grupo1"
                                OnClick="btnAceptar_Click" />
                            <asp:Button ID="btnRegresar" runat="server" Text="Cancelar" OnClick="btnRegresar_Click" />
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
                    ValidationGroup="Grupo1" ShowMessageBox="true" ShowSummary="false" />
            </td>
        </tr>
    </table>
</asp:Content>
