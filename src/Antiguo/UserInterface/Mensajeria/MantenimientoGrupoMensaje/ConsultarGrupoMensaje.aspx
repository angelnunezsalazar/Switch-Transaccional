<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarGrupoMensaje.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoGrupoMensaje.ConsultarGrupoMensaje" %>

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
                <span class="titulo">Grupo de Mensajes</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="dsProtocolo"
                    DataKeyNames="GMJ_CODIGO" Width="900px">
                    <Columns>
                        <asp:BoundField DataField="GMJ_NOMBRE" HeaderText="Nombre" SortExpression="GMJ_NOMBRE" />
                        <asp:BoundField DataField="GMJ_DESCRIPCION" HeaderText="Descripcion" SortExpression="GMJ_DESCRIPCION" ItemStyle-Width="500px"/>
                        <asp:TemplateField HeaderText="TipoMensaje" SortExpression="TIPO_MENSAJE">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("TIPO_MENSAJE.TMJ_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgVer" runat="server" PostBackUrl='<%# "~/Mensajeria/MantenimientoGrupoMensaje/VerEntidadesEnGrupoMensaje.aspx?Codigo="+ Eval("GMJ_CODIGO") %>'
                                    AlternateText="Ver" ImageUrl="~/Includes/Imagenes/iconFind.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" PostBackUrl='<%# "~/Mensajeria/MantenimientoGrupoMensaje/ModificarGrupoMensaje.aspx?Codigo="+ Eval("GMJ_CODIGO") %>'
                                    AlternateText="Modificar" ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" AlternateText="Eliminar"
                                    ImageUrl="~/Includes/Imagenes/iconErase.png" OnClientClick="return confirm('Esta seguro que quiere eliminar el Grupo de Mensaje?')"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsProtocolo" runat="server" DataObjectTypeName="BusinessEntity.GRUPO_MENSAJE"
                    DeleteMethod="eliminarGrupoMensaje" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerGrupoMensaje" TypeName="BusinessLayer.Mensajeria.GrupoMensajeBL"
                    OnDeleted="dsProtocolo_Deleted"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="Button1" runat="server" PostBackUrl="~/Mensajeria/MantenimientoGrupoMensaje/AgregarGrupoMensaje.aspx"
                    Text="Agregar" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
