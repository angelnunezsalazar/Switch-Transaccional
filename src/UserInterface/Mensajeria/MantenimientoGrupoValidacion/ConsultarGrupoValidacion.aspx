<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarGrupoValidacion.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoGrupoValidacion.ConsultarGrupoValidacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <span class="titulo">Consultar Grupos de Validaci&oacute;n</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" align="center" style="width: 400px;">
                    <tr>
                        <td style="width: 110px;" class="form_columna1">
                            <span class="texto1">Grupo Mensaje</span>
                        </td>
                        <td class="form_columna2">
                            <asp:DropDownList ID="drlGrupoMensaje" runat="server" DataSourceID="dsGrupoMensaje"
                                DataTextField="GMJ_NOMBRE" DataValueField="GMJ_CODIGO" AppendDataBoundItems="True"
                                AutoPostBack="True" EnableViewState="False" CssClass="control">
                                <asp:ListItem Value="-1">Seleccionar Grupo</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="dsGrupoMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerGrupoMensaje" TypeName="BusinessLayer.Mensajeria.GrupoMensajeBL">
                            </asp:ObjectDataSource>
                        </td>
                        <td class="form_columna3">
                        </td>
                    </tr>
                    <tr>
                        <td class="form_columna1">
                            <span class="texto1">Mensaje</span>
                        </td>
                        <td class="form_columna2">
                            <asp:DropDownList ID="drlMensaje" runat="server" DataSourceID="dsMensaje" DataTextField="MEN_NOMBRE"
                                DataValueField="MEN_CODIGO" AppendDataBoundItems="True" AutoPostBack="True" EnableViewState="False"
                                CssClass="control">
                                <asp:ListItem Value="-1">Seleccionar Mensaje</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="dsMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerMensajePorCodigoGrupoMensaje" TypeName="BusinessLayer.Mensajeria.MensajeBL">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="drlGrupoMensaje" DefaultValue="-1" Name="codigoGrupoMensaje"
                                        PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                        <td class="form_columna3">
                        </td>
                    </tr>
                    <tr>
                        <td class="form_pie" colspan="3">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <div align="center">
        <asp:GridView ID="grvGruposValidacion" runat="server" AutoGenerateColumns="False"
            DataSourceID="dsGrupoValidacion" DataKeyNames="GRV_CODIGO">
            <Columns>
                <asp:BoundField DataField="GRV_CODIGO" HeaderText="C&oacute;digo" SortExpression="GRV_CODIGO" />
                <asp:BoundField DataField="GRV_NOMBRE" HeaderText="Nombre" SortExpression="GRV_NOMBRE" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgVer" runat="server" ImageUrl="~/Includes/Imagenes/iconFind.png" 
                        PostBackUrl='<%# "~/Mensajeria/MantenimientoGrupoValidacion/ConsultarDetalleReglasValidacion.aspx?codigo="+ Eval("GRV_CODIGO")+"&grupo="+drlGrupoMensaje.SelectedValue +"&mensaje="+drlMensaje.SelectedValue  %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgModificar" runat="server" ImageUrl="~/Includes/Imagenes/iconEdit.png"
                            PostBackUrl='<%# "~/Mensajeria/MantenimientoGrupoValidacion/ModificarGrupoValidacion.aspx?codigo="+ Eval("GRV_CODIGO") %>'
                            AlternateText="Modificar" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEliminar" runat="server" CommandName="Delete" ImageUrl="~/Includes/Imagenes/iconErase.png" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="dsGrupoValidacion" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="obtenerGrupoValidacion" TypeName="BusinessLayer.Mensajeria.GrupoValidacionBL"
            DataObjectTypeName="BusinessEntity.GRUPO_VALIDACION" DeleteMethod="eliminarGrupoValidacion"
            OnDeleted="dsGrupoValidacion_Deleted">
            <SelectParameters>
                <asp:ControlParameter ControlID="drlMensaje" DefaultValue="0" Name="mensaje" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <br />
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" Visible="False" OnClick="btnNuevo_Click" />
        <br />
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
