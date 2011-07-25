<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarProtocolo.aspx.cs" Inherits="UserInterface.Comunicacion.MantenimientoProtocolo.ConsultarProtocolo" %>

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
                <span class="titulo">Consultar Protocolo</span>
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
                    DataKeyNames="PTR_CODIGO" HorizontalAlign="Center">
                    <HeaderStyle CssClass="tabla_cabecera" />
                    <RowStyle CssClass="tabla_fila_impar" />
                    <AlternatingRowStyle CssClass="tabla_fila_par" />
                    <Columns>
                        <asp:BoundField DataField="PTR_NOMBRE" HeaderText="Nombre" SortExpression="PTR_NOMBRE" />
                        <asp:BoundField DataField="PTR_TIMEOUT_REQUEST" HeaderText="TimeOut Request" SortExpression="PTR_TIMEOUT_REQUEST" />
                        <asp:BoundField DataField="PTR_TIMEOUT_RESPONSE" HeaderText="TimeOut Response" SortExpression="PTR_TIMEOUT_RESPONSE" />
                        <asp:TemplateField HeaderText="Tipo Comunicaci&oacute;n" SortExpression="TipoComunicacion">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("TIPO_COMUNICACION.TPO_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CheckBoxField DataField="PTR_INICIA_COMM" HeaderText="Inicia Comunicaci&oacute;n"
                            SortExpression="PTR_INICIA_COMM" />
                        <asp:CheckBoxField DataField="PTR_ACEPTA_COMM" HeaderText="Acepta Comunicaci&oacute;n"
                            SortExpression="PTR_ACEPTA_COMM" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgModificar" runat="server" PostBackUrl='<%# "~/Comunicacion/MantenimientoProtocolo/ModificarProtocolo.aspx?Codigo="+ Eval("PTR_CODIGO") %>'
                                    AlternateText="Modificar" ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEliminar" runat="server" CommandName="Delete" AlternateText="Eliminar"
                                    ImageUrl="~/Includes/Imagenes/iconErase.png" OnClientClick="return confirm('Esta seguro que quiere eliminar el protocolo?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsProtocolo" runat="server" DataObjectTypeName="BusinessEntity.PROTOCOLO"
                    DeleteMethod="eliminarProtocolo" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerProtocolo" TypeName="BusinessLayer.Comunicacion.ProtocoloBL"
                    OnDeleted="dsProtocolo_Deleted"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="Button1" runat="server" PostBackUrl="~/Comunicacion/MantenimientoProtocolo/AgregarProtocolo.aspx"
                    Text="Agregar" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
