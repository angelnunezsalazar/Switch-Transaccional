<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarTransformacionCampo.aspx.cs" Inherits="UserInterface.Operacion.MantenimientoTransformacionCampo.ConsultarTransformacionCampo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
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
                <span class="titulo">Consultar Transformación Campo</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:FormView ID="frmTransformacion" runat="server" DataSourceID="dsTransformacion"
        HorizontalAlign="Center">
        <ItemTemplate>
            <table class="style1">
                <tr>
                    <td>
                        <span class="texto">Transformada</span>
                    </td>
                    <td>
                        <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("TRM_NOMBRE") %>' CssClass="texto" />
                        <asp:HiddenField ID="hndCodigoMensajeOrigen" runat="server" Value='<%# Bind("TRM_CODIGO") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="texto">Grupo Mensaje Origen</span>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("MENSAJE_ORIGEN.GRUPO_MENSAJE.GMJ_NOMBRE") %>'
                            CssClass="texto"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="texto">Mensaje Origen</span>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("MENSAJE_ORIGEN.MEN_NOMBRE") %>'
                            CssClass="texto" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="texto">Grupo Mensaje Destino</span>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("MENSAJE_DESTINO.GRUPO_MENSAJE.GMJ_NOMBRE") %>'
                            CssClass="texto"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="texto">Mensaje Destino</span>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("MENSAJE_DESTINO.MEN_NOMBRE") %>'
                            CssClass="texto" />
                    </td>
                </tr>
            </table>
            <%-- <table class="style1">
                <tr>
                    <td>
                        <span class="texto">Grupo Mensaje Origen</span>
                    </td>
                    <td>
                        <asp:Label ID="lblGrupoOrigen" runat="server" Text='<%# Bind("MENSAJE_ORIGEN.GRUPO_MENSAJE.GMJ_NOMBRE") %>'></asp:Label>
                    </td>
                    <td>
                        <span class="texto">Grupo Mensaje Destino</span>
                    </td>
                    <td>
                        <asp:Label ID="lblGrupoFin" runat="server" Text='<%# Bind("MENSAJE_DESTINO.GRUPO_MENSAJE.GMJ_NOMBRE") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="texto">Mensaje Origen</span>
                    </td>
                    <td>
                        <asp:Label ID="lblMensajeOrigen" runat="server" Text='<%# Bind("MENSAJE_ORIGEN.MEN_NOMBRE") %>' />
                    </td>
                    <td>
                        <span class="texto">Mensaje Destino</span>
                    </td>
                    <td>
                        <asp:Label ID="lblMensajeDestino" runat="server" Text='<%# Bind("MENSAJE_DESTINO.MEN_NOMBRE") %>' />
                    </td>
                </tr>
            </table>--%>
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="dsTransformacion" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="obtenerTransformacion" TypeName="BusinessLayer.Operacion.TransformacionMensajeBL">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="codigoTransformacion" QueryStringField="Transformacion"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table class="style1">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grvCampoDestino" runat="server" AutoGenerateColumns="False" DataSourceID="dsCampoDestino"
                    DataKeyNames="CAM_CODIGO,MEN_CODIGO" 
                    onrowcommand="grvCampoDestino_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="CAM_NOMBRE" HeaderText="CampoDestino" SortExpression="CAM_NOMBRE" />
                        <asp:TemplateField HeaderText="TipoDato" SortExpression="TIPO_DATO">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("TIPO_DATO.TDT_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CAM_LONGITUD" HeaderText="Longitud" SortExpression="CAM_LONGITUD" />
                        <asp:CheckBoxField DataField="CAM_REQUERIDO" HeaderText="Requerido" SortExpression="CAM_REQUERIDO" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="VerDetalles"
                                    AlternateText="Modificar" ImageUrl="~/Includes/Imagenes/iconFind.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsCampoDestino" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerCampo" TypeName="BusinessLayer.Mensajeria.CampoMensajeBL">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="codigoMensaje" QueryStringField="Mensaje"
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
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" PostBackUrl="~/Operacion/MantenimientoTransformacionMensaje/ConsultarTransformacionMensaje.aspx"/>
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
