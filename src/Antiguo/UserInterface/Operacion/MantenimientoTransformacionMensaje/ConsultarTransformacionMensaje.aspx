<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarTransformacionMensaje.aspx.cs" Inherits="UserInterface.Operacion.MantenimientoTransformacionMensaje.ConsultarTransformacionMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .columna1
        {
            width: 150px;
        }
        .columna2
        {
            width: 135px;
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
                <span class="titulo">Consultar Transformación Mensaje</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: 690px">
        <tr>
            <td class="columna1">
                <span class="texto">Nombre Transformada</span>
            </td>
            <td style="width: 540px" colspan="3">
                <asp:TextBox ID="txtNombre" runat="server" Width="190px" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table class="style1" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td class="columna1">
                            <span class="texto">Grupo Mensaje Inicio</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlGrupoMensajeInicio" runat="server" DataSourceID="dsGrupoMensaje"
                                DataTextField="GMJ_NOMBRE" DataValueField="GMJ_CODIGO" AppendDataBoundItems="True" AutoPostBack="True">
                                <asp:ListItem Value="-1">Todos los Grupos</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="dsGrupoMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerGrupoMensaje" TypeName="BusinessLayer.Mensajeria.GrupoMensajeBL">
                            </asp:ObjectDataSource>
                        </td>
                        <td class="columna2">
                            <span class="texto">Grupo Mensaje Fin</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drlGrupoMensajeFin" runat="server" DataSourceID="dsGrupoMensaje"
                                DataTextField="GMJ_NOMBRE" DataValueField="GMJ_CODIGO" AutoPostBack="True" AppendDataBoundItems="True">
                                <asp:ListItem Value="-1">Todos los grupos</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="columna1">
                            <span class="texto">Mensaje Inicio</span>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="udpMensajeInicio" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="drlMensajeInicio" runat="server" DataSourceID="dsMensajeInicio"
                                        DataTextField="MEN_NOMBRE" DataValueField="MEN_CODIGO" AppendDataBoundItems="True"
                                        EnableViewState="False">
                                        <asp:ListItem Value="-1">Todos los Mensajes</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="dsMensajeInicio" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="obtenerMensajePorCodigoGrupoMensaje" TypeName="BusinessLayer.Mensajeria.MensajeBL">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="drlGrupoMensajeInicio" DefaultValue="-1" Name="codigoGrupoMensaje"
                                                PropertyName="SelectedValue" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="drlGrupoMensajeInicio" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td class="columna2">
                            <span class="texto">Mensaje Fin</span>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="udpMensajeFin" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="drlMensajeFin" runat="server" DataSourceID="dsMensajeFin" DataTextField="MEN_NOMBRE"
                                        DataValueField="MEN_CODIGO" AppendDataBoundItems="True" EnableViewState="False">
                                        <asp:ListItem Value="-1">Todos los Mensajes</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="dsMensajeFin" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="obtenerMensajePorCodigoGrupoMensaje" TypeName="BusinessLayer.Mensajeria.MensajeBL">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="drlGrupoMensajeFin" DefaultValue="-1" Name="codigoGrupoMensaje"
                                                PropertyName="SelectedValue" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="drlGrupoMensajeFin" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
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
            <td colspan="4" style="text-align: center">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                    onclick="btnBuscar_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                <asp:GridView ID="grvTransformacion" runat="server" AutoGenerateColumns="False" DataSourceID="dsTransformacion"
                    DataKeyNames="TRM_CODIGO" onrowcommand="grvTransformacion_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Nombre" SortExpression="TRM_NOMBRE">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("TRM_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GrupoOrigen" SortExpression="MENSAJE_ORIGEN">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("MENSAJE_ORIGEN.GRUPO_MENSAJE.GMJ_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MensajeOrigen" SortExpression="MENSAJE_DESTINO">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("MENSAJE_ORIGEN.MEN_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GrupoDestino" SortExpression="MENSAJE_ORIGEN">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("MENSAJE_DESTINO.GRUPO_MENSAJE.GMJ_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MensajeDestino" SortExpression="MENSAJE_DESTINO">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("MENSAJE_DESTINO.MEN_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgFind" runat="server" CommandName="VerDetalles" CommandArgument='<%# Eval("TRM_CODIGO") + "|" + Eval("MENSAJE_DESTINO.MEN_CODIGO") %>'
                                    AlternateText="VerCampos" ImageUrl="~/Includes/Imagenes/iconFind.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" PostBackUrl='<%# "~/Operacion/MantenimientoTransformacionMensaje/ModificarTransformacionMensaje.aspx?Transformacion="+ Eval("TRM_CODIGO") %>'
                                    AlternateText="Modificar" ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEliminar" runat="server" CommandName="Delete" AlternateText="Eliminar"
                                    ImageUrl="~/Includes/Imagenes/iconErase.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsTransformacion" runat="server" DataObjectTypeName="BusinessEntity.TRANSFORMACION"
                    DeleteMethod="eliminarTransformacion" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerTransformacion" 
                    TypeName="BusinessLayer.Operacion.TransformacionMensajeBL" 
                    onselecting="dsTransformacion_Selecting" 
                    ondeleted="dsTransformacion_Deleted">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtNombre" DefaultValue="%" Name="nombreTransformada"
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="drlGrupoMensajeInicio" DefaultValue="%" Name="codigoGrupoOrigen"
                            PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter ControlID="drlMensajeInicio" DefaultValue="%" Name="codigoMensajeOrigen"
                            PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter ControlID="drlGrupoMensajeFin" DefaultValue="%" Name="codigoGrupoDestino"
                            PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter ControlID="drlMensajeFin" DefaultValue="%" Name="codigoMensajeDestino"
                            PropertyName="SelectedValue" Type="Int32" />
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
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                    onclick="btnAgregar_Click" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
