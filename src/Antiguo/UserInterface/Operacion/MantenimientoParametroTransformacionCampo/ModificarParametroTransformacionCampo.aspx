<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ModificarParametroTransformacionCampo.aspx.cs" Inherits="UserInterface.Operacion.MantenimientoParametroTransformacionCampo.ModificarParametroTransformacionCampo" %>

<%@ Import Namespace="BusinessLayer.Operacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .columna1
        {
            width: 160px;
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
                <span class="titulo">Modificar Parametro Campo</span>
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
                        <table class="style1">
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Transformada</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("TRM_NOMBRE") %>' CssClass="texto" />
                                    <asp:HiddenField ID="hndCodigoMensajeOrigen" runat="server" Value='<%# Bind("TRM_CODIGO") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Grupo Mensaje Origen</span>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("MENSAJE_ORIGEN.GRUPO_MENSAJE.GMJ_NOMBRE") %>'
                                        CssClass="texto"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Mensaje Origen</span>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("MENSAJE_ORIGEN.MEN_NOMBRE") %>'
                                        CssClass="texto" />
                                </td>
                            </tr>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Grupo Mensaje Destino</span>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("MENSAJE_DESTINO.GRUPO_MENSAJE.GMJ_NOMBRE") %>'
                                        CssClass="texto"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Mensaje Destino</span>
                                </td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("MENSAJE_DESTINO.MEN_NOMBRE") %>'
                                        CssClass="texto" />
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
                <asp:FormView ID="frmTransformacionCampo" runat="server" DataSourceID="dsTransformacionCampo">
                    <ItemTemplate>
                        <table class="style1">
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Campo Destino</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblCampoDestino" CssClass="texto" runat="server" Text='<%# Bind("CAMPO_DESTINO.CAM_NOMBRE") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Tipo Transformacion</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblTipoTransformacion" CssClass="texto" runat="server" Text='<%# ObtenerNombreTipoTransformacion(Eval("TCM_TIPO").ToString()) %>' />
                                    <asp:HiddenField ID="hdnCodigoTipoTransformacion" runat="server" Value='<%# Bind("TCM_TIPO") %>' />
                                </td>
                            </tr>
                            <%if (((HiddenField)this.frmTransformacionCampo.FindControl("hdnCodigoTipoTransformacion")).Value == TipoTransformacionBL.obtenerCodigoProcedimientoAlmacenado().ToString())
                              {%>
                            <tr>
                                <td class="columna1">
                                    <span class="texto">Transformacion Campo</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblProcedimiento" CssClass="texto" runat="server" Text='<%# Bind("TCM_PROCEDIMIENTO") %>' />
                                </td>
                            </tr>
                            <% } %>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="dsTransformacionCampo" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerTransformacionCampoConCampoDestino" TypeName="BusinessLayer.Operacion.TransformacionCampoBL">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoTransformacion" QueryStringField="Transformacion"
                            Type="Int32" />
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoMensaje" QueryStringField="Mensaje"
                            Type="Int32" />
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoCampo" QueryStringField="Campo"
                            Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <table>
                    <tr>
                        <td class="columna1">
                            <span class="texto">Tipo Parámetro</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlTipoParametro" runat="server" DataSourceID="dsTipoParametroTransformacionCampo"
                                DataTextField="Value" DataValueField="Key" AutoPostBack="True" AppendDataBoundItems="True"
                                Width="200px">
                                <asp:ListItem Value="-1">Seleccionar Tipo Parametro</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drlTipoParametro"
                                ErrorMessage="Debe ingresar el Tipo Parámetro" InitialValue="-1" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="dsTipoParametroTransformacionCampo" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerTipoTransformacionCampo" TypeName="BusinessLayer.Operacion.TipoParametroTransformacionCampoBL">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <%if (this.drlTipoParametro.SelectedValue == TipoParametroTransformacionCampoBL.obtenerCodigoCampoOrigen().ToString() ||
                                this.drlTipoParametro.SelectedValue == TipoParametroTransformacionCampoBL.obtenerCodigoTabla().ToString())
                      {%>
                    <tr>
                        <td class="columna1">
                            <span class="texto">Campo Origen</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlCampoOrigen" runat="server" DataSourceID="dsCampoOrigen"
                                DataTextField="CAM_NOMBRE" DataValueField="CAM_CODIGO" AppendDataBoundItems="True"
                                Width="200px">
                                <asp:ListItem Value="-1">Seleccionar Campo Origen</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCampoOrigen" runat="server" ControlToValidate="drlCampoOrigen"
                                ErrorMessage="Debe ingresar el Campo Origen" InitialValue="-1" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="dsCampoOrigen" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerCampoOrigenPorTransaccion" TypeName="BusinessLayer.Mensajeria.CampoMensajeBL"
                                OnSelected="dsCampoOrigen_Selected">
                                <SelectParameters>
                                    <asp:QueryStringParameter DefaultValue="-1" Name="codigoTransaccion" QueryStringField="Transformacion"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="columna1">
                            <span class="texto">Posición Inicial</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPosicionInicial" runat="server" Text='<%# Bind("PTC_POSICION_INICIAL") %>'
                                MaxLength="4" Width="50px" />
                            <asp:RequiredFieldValidator ID="rfvPosicionInicial" runat="server" ControlToValidate="txtPosicionInicial"
                                Display="Dynamic" ErrorMessage="Debe ingresar la Posicion Inicial" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revPosicionInicial" runat="server" ControlToValidate="txtPosicionInicial"
                                ErrorMessage="Debe ingresar una Posición Inicial válida" ValidationExpression="\d*"
                                ValidationGroup="Grupo">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="columna1">
                            <span class="texto">Longitud</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLongitud" runat="server" MaxLength="4" Width="50px" />
                            <asp:RequiredFieldValidator ID="rfvPosicionInicial0" runat="server" ControlToValidate="txtLongitud"
                                Display="Dynamic" ErrorMessage="Debe ingresar la Longitud" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revPosicionInicial0" runat="server" ControlToValidate="txtLongitud"
                                ErrorMessage="Debe ingresar una Longitud válida" ValidationExpression="\d*" ValidationGroup="Grupo">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <%if (this.drlTipoParametro.SelectedValue == TipoParametroTransformacionCampoBL.obtenerCodigoTabla().ToString())
                      {%>
                    <tr>
                        <td class="columna1">
                            <span class="texto">Tabla</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlTabla" runat="server" DataSourceID="dsTabla" DataTextField="TBL_NOMBRE"
                                DataValueField="TBL_CODIGO" AppendDataBoundItems="True" AutoPostBack="true" Width="200px">
                                <asp:ListItem Value="-1">Seleccionar Tabla</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvTabla" runat="server" ControlToValidate="drlTabla"
                                ErrorMessage="Debe ingresar la Tabla" InitialValue="-1" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="dsTabla" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerTabla" TypeName="BusinessLayer.Mensajeria.TablaBL"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="columna1">
                            <span class="texto">Columna Origen</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlColumnaOrigen" runat="server" DataSourceID="dsColumnaOrigen"
                                DataTextField="COL_NOMBRE" DataValueField="COL_CODIGO" AppendDataBoundItems="True"
                                EnableViewState="False" Width="200px">
                                <asp:ListItem Value="-1">Seleccionar Columna Origen</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drlColumnaOrigen"
                                ErrorMessage="Debe ingresar la Columna Origen" InitialValue="-1" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="dsColumnaOrigen" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerColumna" TypeName="BusinessLayer.Mensajeria.ColumnaBL">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="drlTabla" DefaultValue="-1" Name="codigoTabla" PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="columna1">
                            <span class="texto">Columna Destino</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlColumnaDestino" runat="server" DataSourceID="dsColumnaOrigen"
                                DataTextField="COL_NOMBRE" DataValueField="COL_CODIGO" AppendDataBoundItems="True"
                                EnableViewState="False" Width="200px">
                                <asp:ListItem Value="-1">Seleccionar Columna Destino</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drlColumnaDestino"
                                ErrorMessage="Debe ingresar la Columna Destino" InitialValue="-1" ValidationGroup="Grupo">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <% } %>
                    <% } %>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" ValidationGroup="Grupo"
                                OnClick="btnAceptar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
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
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary runat="server" ID="vsmParametroCampo" ValidationGroup="Grupo"
                    HeaderText="Se han producido los siguientes errores:" ShowMessageBox="true" ShowSummary="false" />
            </td>
        </tr>
    </table>
</asp:Content>
