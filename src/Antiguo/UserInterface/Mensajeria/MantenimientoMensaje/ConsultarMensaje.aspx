<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarMensaje.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoMensaje.ConsultarMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="style2">
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <span class="titulo">Consultar Mensaje </span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: 35%">
        <tr>
            <td>
                <span class="texto">Grupo de Mensaje</span>
            </td>
            <td>
                <asp:DropDownList ID="drlMensaje" runat="server" DataSourceID="dsGrupoMensaje"
                    DataTextField="GMJ_NOMBRE" DataValueField="GMJ_CODIGO" AppendDataBoundItems="true"
                    AutoPostBack="True" OnDataBound="DropDownList1_DataBound">
                    <asp:ListItem Value="-1">Seleccionar Grupo de Mensaje</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="dsGrupoMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerGrupoMensaje" TypeName="BusinessLayer.Mensajeria.GrupoMensajeBL">
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table class="style2">
        <tr>
            <td>
                <asp:GridView ID="grdMensaje" runat="server" AutoGenerateColumns="False" DataSourceID="dsMensaje"
                    DataKeyNames="MEN_CODIGO" OnDataBound="grdMensaje_DataBound">
                    <Columns>
                        <asp:BoundField DataField="MEN_NOMBRE" HeaderText="Nombre" SortExpression="MEN_NOMBRE">
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" PostBackUrl='<%# "~/Mensajeria/MantenimientoCampoMensaje/ConsultarCampoMensaje.aspx?Codigo="+ Eval("MEN_CODIGO")+"&GrupoMensaje="+drlMensaje.SelectedValue %>'
                                    AlternateText="Ver" ImageUrl="~/Includes/Imagenes/iconFind.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" PostBackUrl='<%# "~/Mensajeria/MantenimientoMensaje/ModificarMensaje.aspx?Codigo="+ Eval("MEN_CODIGO") %>'
                                    AlternateText="Modificar" ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Delete" AlternateText="Eliminar"
                                    ImageUrl="~/Includes/Imagenes/iconErase.png" OnClientClick="return confirm('Esta seguro que quiere eliminar el Mensaje?')"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsMensaje" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerMensajePorCodigoGrupoMensaje" TypeName="BusinessLayer.Mensajeria.MensajeBL"
                    DataObjectTypeName="BusinessEntity.MENSAJE" DeleteMethod="eliminarMensaje" OnDeleted="dsMensaje_Deleted">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drlMensaje" DefaultValue="-1" Name="codigoGrupoMensaje"
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
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
