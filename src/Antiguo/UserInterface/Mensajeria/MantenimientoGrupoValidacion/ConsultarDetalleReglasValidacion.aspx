<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarDetalleReglasValidacion.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoGrupoValidacion.ConsultarDetalleReglasValidacion" %>

<%@ Import Namespace="BusinessLayer.Mensajeria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center" style="width: 500px;">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <span class="titulo">Consultar Detalle de Reglas de Validaci&oacute;n</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table align="center" style="width: 500px;">
                    <tr>
                        <td style="width: 150px;">
                            <span class="texto">Nombre</span>
                        </td>
                        <td>
                            <asp:Label ID="lblNombre" CssClass="texto" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:FormView ID="frmGrupoMensaje" runat="server" DataSourceID="dsGrupoMensaje">
                    <ItemTemplate>
                        <table align="center" style="width: 500px;">
                            <tr>
                                <td style="width: 150px;">
                                    <span class="texto">Grupo de Mensajes</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblNombreGrupoMensaje" runat="server" CssClass="texto" Text='<%# Bind("GMJ_NOMBRE") %>' />
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
                        <table align="center" style="width: 500px;">
                            <tr>
                                <td style="width: 150px;">
                                    <span class="texto">Mensaje</span>
                                </td>
                                <td>
                                    <asp:Label ID="lblNombreMensaje" runat="server" CssClass="texto" Text='<%# Bind("MEN_NOMBRE") %>' />
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
                <table align="center" style="width: 500px;">
                    <tr>
                        <td style="width: 150px;">
                            <span class="texto">Campo</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlCampo" runat="server" DataSourceID="dsCampo" DataTextField="CAM_NOMBRE"
                                DataValueField="CAM_CODIGO" AppendDataBoundItems="True" AutoPostBack="True">
                                <asp:ListItem Selected="True" Value="0">Seleccione Campo</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="dsCampo" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerCampo" TypeName="BusinessLayer.Mensajeria.CampoMensajeBL">
                                <SelectParameters>
                                    <asp:QueryStringParameter DefaultValue="0" Name="codigoMensaje" QueryStringField="mensaje"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="grvReglasCampo" runat="server" AutoGenerateColumns="False" DataSourceID="dsValidacionCampo"
        OnRowDataBound="grvReglasCampo_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Tipo de Regla">
                <ItemTemplate>
                    <asp:Label ID="lblTipoRegla" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tipo de Validación">
                <ItemTemplate>
                    <asp:Label ID="lblTipoValidacion" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Valor de Referencia">
                <ItemTemplate>
                    <asp:Label ID="lblValorReferencia" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Campo Tabla">
                <ItemTemplate>
                    <asp:Label ID="lblCampoTabla" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgModificar" runat="server" ImageUrl="~/Includes/Imagenes/iconEdit.png"
                        PostBackUrl='<%# "~/Mensajeria/MantenimientoGrupoValidacion/ModificarValidacionCampo.aspx?vcampo="+ Eval("VLC_CODIGO") + "&codigo="+ Request.QueryString["codigo"] + "&mensaje=" + Request.QueryString["mensaje"] + "&campo=" + this.drlCampo.SelectedValue %>'
                        AlternateText="Modificar" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgEliminar" runat="server" CommandName="Delete" ImageUrl="~/Includes/Imagenes/iconErase.png"
                        OnClientClick="return confirm('¿Está seguro de que quiere eliminar la Tabla?')" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="dsValidacionCampo" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="obtenerValidacionCampo" TypeName="BusinessLayer.Mensajeria.ValidacionCampoBL"
        DataObjectTypeName="BusinessEntity.VALIDACION_CAMPO" DeleteMethod="eliminarValidacionCampo"
        OnDeleted="dsValidacionCampo_Deleted">
        <SelectParameters>
            <asp:QueryStringParameter Name="grupoVal" QueryStringField="codigo" Type="Int32" />
            <asp:QueryStringParameter Name="mensaje" QueryStringField="mensaje" Type="Int32" />
            <asp:ControlParameter ControlID="drlCampo" Name="campo" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <div align="center">
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    </div>
    <div align="center">
        <asp:Button ID="btnAgregar" Visible="false" runat="server" Text="Agregar" OnClick="btnAgregar_Click" /></div>
</asp:Content>
