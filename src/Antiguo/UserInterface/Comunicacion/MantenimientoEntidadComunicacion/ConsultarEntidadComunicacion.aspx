<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarEntidadComunicacion.aspx.cs" Inherits="UserInterface.Comunicacion.MantenimientoEntidadComunicacion.ConsultarEntidadComunicacion" %>

<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="style1">
        <tr>
            <td>
                <table class="style1">
                    <tr>
                        <td style="text-align: center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <span class="titulo">Entidad Comunicacion</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="dsEntidadComunicacion"
                                DataKeyNames="EDC_CODIGO" Width="650px">
                                <Columns>
                                    <asp:BoundField DataField="EDC_NOMBRE" HeaderText="Nombre" SortExpression="EDC_NOMBRE" />
                                    <asp:BoundField DataField="EDC_TIMEOUT_EN_COLA" HeaderText="TimeOut Cola" SortExpression="EDC_TIMEOUT_EN_COLA" />
                                    <asp:TemplateField HeaderText="Protocolo" SortExpression="Protocolo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProtocolo" runat="server" Text='<%# Bind("Protocolo.PTR_NOMBRE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TipoEntidad" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTipoEntidad" runat="server" Text='<%# Bind("TipoEntidad.TEM_NOMBRE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgModificar" runat="server" PostBackUrl='<%# "~/Comunicacion/MantenimientoEntidadComunicacion/ModificarEntidadComunicacion.aspx?Codigo="+ Eval("EDC_CODIGO") %>'
                                                AlternateText="Modificar" ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEliminar" runat="server" CommandName="Delete" AlternateText="Eliminar"
                                                ImageUrl="~/Includes/Imagenes/iconErase.png" OnClientClick="return confirm('Esta seguro que quiere eliminar la Entidad Comunicacion?')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="dsEntidadComunicacion" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="obtenerEntidadComunicacion" TypeName="BusinessLayer.Comunicacion.EntidadComunicacionBL"
                                DataObjectTypeName="BusinessEntity.EntidadComunicacion" DeleteMethod="eliminarEntidadComunicacion"
                                OnDeleted="dsEntidadComunicacion_Deleted"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="Button1" runat="server" PostBackUrl="~/Comunicacion/MantenimientoEntidadComunicacion/AgregarEntidadComunicacion.aspx"
                                Text="Agregar" />
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
            </td>
        </tr>
    </table>
</asp:Content>
