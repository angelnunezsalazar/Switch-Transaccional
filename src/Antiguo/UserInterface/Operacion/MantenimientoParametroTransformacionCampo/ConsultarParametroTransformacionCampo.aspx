<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarParametroTransformacionCampo.aspx.cs" Inherits="UserInterface.Operacion.MantenimientoParametroTransformacionCampo.ConsultarParametroTransformacionCampo" %>

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
                <span class="titulo">Consultar Parametro Campo</span>
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
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="dsParametroTransformacionCampo"
                    DataKeyNames="PTC_CODIGO" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="TipoParametro" SortExpression="PTC_TIPO">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoParametroTransformacionCampo" runat="server" Text='<%# TipoParametroTransformacionCampoBL.obtenerNombrePorCodigo(Convert.ToInt32(Eval("PTC_TIPO"))) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CampoOrigen" SortExpression="CAMPO">
                            <ItemTemplate>
                                <asp:Label ID="lblCampo" runat="server" Text='<%# Bind("CAMPO.CAM_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PTC_POSICION_INICIAL" HeaderText="PosicionInicial" SortExpression="PTC_POSICION_INICIAL" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="PTC_LONGITUD" HeaderText="Longitud" SortExpression="PTC_LONGITUD" ItemStyle-HorizontalAlign="Center"/>
                        <asp:TemplateField HeaderText="Tabla" SortExpression="TABLA">
                            <ItemTemplate>
                                <asp:Label ID="lblTabla" runat="server" Text='<%# Bind("TABLA.TBL_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ColumnaOrigen" SortExpression="COLUMNA_ORIGEN">
                            <ItemTemplate>
                                <asp:Label ID="lblColumnaOrigen" runat="server" Text='<%# Bind("COLUMNA_ORIGEN.COL_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ColumnaDestino" SortExpression="COLUMNA_DESTINO">
                            <ItemTemplate>
                                <asp:Label ID="lblColumnaDestino" runat="server" Text='<%# Bind("COLUMNA_DESTINO.COL_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgModificar" runat="server" ImageUrl="~/Includes/Imagenes/iconEdit.png"
                                    CommandArgument='<%#Eval("PTC_CODIGO") %>' AlternateText="Modificar" CommandName="Modificar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEliminar" runat="server" CommandName="Delete" ImageUrl="~/Includes/Imagenes/iconErase.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsParametroTransformacionCampo" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerParametroTransformacionCampo" TypeName="BusinessLayer.Operacion.ParametroTransformacionCampoBL"
                    DataObjectTypeName="BusinessEntity.PARAMETRO_TRANSFORMACION_CAMPO" DeleteMethod="eliminarParametroTransformacionCampo"
                    OnDeleted="dsParametroTransformacionCampo_Deleted">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoTransformacion" QueryStringField="Transformacion"
                            Type="Int32" />
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoMensaje" QueryStringField="Mensaje"
                            Type="Int32" />
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoCampo" QueryStringField="Campo"
                            Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" Text="Agregar" />
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" 
                    onclick="btnRegresar_Click" />
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
