<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="AgregarValidacionCampo.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoGrupoValidacion.AgregarValidacionCampo" %>

<%@ Import Namespace="BusinessLayer.Mensajeria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center" style="width: 400px;">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <span class="titulo">Agregar Regla de Validaci&oacute;n a Campo</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table align="center" style="width: 400px;">
                    <tr>
                        <td style="width: 160px;">
                            <span class="texto">Grupo de Validaci&oacute;n</span>
                        </td>
                        <td>
                            <asp:Label ID="lblNombreGrupo" CssClass="texto" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:FormView ID="frmGrupoMensaje" runat="server" DataSourceID="dsGrupoMensaje">
                    <ItemTemplate>
                        <table align="center" style="width: 400px;">
                            <tr>
                                <td style="width: 160px;">
                                    <span class="texto">Grupo de Mensajes</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblNombreGrupoMensaje" CssClass="texto" runat="server" Text='<%# Bind("GMJ_NOMBRE") %>' />
                                    <asp:HiddenField ID="hdnCodigoGrupoMensaje" Value='<%# Bind("GMJ_CODIGO") %>' runat="server" />
                                    <asp:HiddenField ID="hdnTipoMensajeCodigo" runat="server" Value='<%# Bind("TIPO_MENSAJE.TMJ_CODIGO") %>' />
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
                <asp:FormView ID="frmMensaje" runat="server" DataSourceID="dsMensaje">
                    <ItemTemplate>
                        <table align="center" style="width: 400px;">
                            <tr>
                                <td style="width: 160px;">
                                    <span class="texto">Mensaje</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblNombreMensaje" CssClass="texto" runat="server" Text='<%# Bind("MEN_NOMBRE") %>' />
                                    <asp:HiddenField ID="hdnCodigoMensaje" Value='<%# Bind("MEN_CODIGO") %>' runat="server" />
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
                <table align="center" style="width: 400px;">
                    <tr>
                        <td style="width: 160px;">
                            <span class="texto">Campo</span>
                        </td>
                        <td>
                            <asp:Label ID="lblNombreCampo" CssClass="texto" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="texto">Criterio de Aplicaci&oacute;n</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCriterio" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="2">Inclusión</asp:ListItem>
                                <asp:ListItem Value="3">Procedimiento</asp:ListItem>
                                <asp:ListItem Value="4">Componente</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <%if (this.ddlCriterio.SelectedValue == ValidacionCampoBL.obtenerCriterioAplicacionInclusion().ToString())
                      {%>
                    <tr>
                        <td>
                            <span class="texto">Tipo de Regla</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipoRegla" runat="server" AutoPostBack="True">
                                <asp:ListItem Selected="True" Value="1">Condición</asp:ListItem>
                                <asp:ListItem Value="2">Tabla de valores</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <%}%>
                    <%if (this.ddlCriterio.SelectedValue == ValidacionCampoBL.obtenerCriterioAplicacionInclusion().ToString() && (this.ddlTipoRegla.SelectedValue == ValidacionCampoBL.obtenerTipoReglaCondicion().ToString()))
                      {%>
                    <tr>
                        <td>
                            <span class="texto">Condici&oacute;n</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCondicion" runat="server">
                                <asp:ListItem Selected="True" Value="1">=</asp:ListItem>
                                <asp:ListItem Value="2">&lt;&gt;</asp:ListItem>
                                <asp:ListItem Value="3">&lt;</asp:ListItem>
                                <asp:ListItem Value="4">&lt;=</asp:ListItem>
                                <asp:ListItem Value="5">&gt;</asp:ListItem>
                                <asp:ListItem Value="6">&gt;=</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="texto">Valor de Comparaci&oacute;n</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtValorComparacion" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <%}%>
                    <%if (this.ddlCriterio.SelectedValue == ValidacionCampoBL.obtenerCriterioAplicacionInclusion().ToString() && (this.ddlTipoRegla.SelectedValue == ValidacionCampoBL.obtenerTipoReglaTablaValores().ToString()))
                      {%>
                    <tr>
                        <td>
                            <span class="texto">Nombre de la Tabla</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNombreTabla" runat="server" AppendDataBoundItems="True"
                                AutoPostBack="True" DataSourceID="dsTabla" DataTextField="TBL_NOMBRE" DataValueField="TBL_CODIGO">
                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="dsTabla" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerTabla" TypeName="BusinessLayer.Mensajeria.TablaBL"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="texto">Campo de la Tabla</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCampoTabla" runat="server" AppendDataBoundItems="True" DataSourceID="dsColumna"
                                DataTextField="COL_NOMBRE" DataValueField="COL_CODIGO">
                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="dsColumna" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerColumna" TypeName="BusinessLayer.Mensajeria.ColumnaBL">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlNombreTabla" Name="codigoTabla" PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <%}%>
                    <%if (this.ddlCriterio.SelectedValue == ValidacionCampoBL.obtenerCriterioAplicacionProcedimiento().ToString())
                      {%>
                    <tr>
                        <td>
                            <span class="texto">Nombre Procedimiento</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtProcedure" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <%}%>
                    <%if (this.ddlCriterio.SelectedValue == ValidacionCampoBL.obtenerCriterioAplicacionComponente().ToString())
                      {%>
                    <tr>
                        <td>
                            <span class="texto">Componente</span>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="texto">Clase</span>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="texto">M&eacute;todo</span>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <%}%>
                </table>
                <table align="center" style="width: 400px;">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" PostBackUrl="~/Mensajeria/MantenimientoGrupoValidacion/ConsultarGrupoValidacion.aspx"
                                OnClick="btnCancelar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
