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
                    DataKeyNames="Id" HorizontalAlign="Center">
                    <HeaderStyle CssClass="tabla_cabecera" />
                    <RowStyle CssClass="tabla_fila_impar" />
                    <AlternatingRowStyle CssClass="tabla_fila_par" />
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="TimeoutRequest" HeaderText="Timeout Request" />
                        <asp:BoundField DataField="TimeoutResponse" HeaderText="Timeout Response" />
                        <asp:TemplateField HeaderText="Tipo Comunicaci&oacute;n" SortExpression="TipoComunicacion">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("TipoComunicacion.Nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CheckBoxField DataField="IniciaComunicacion" HeaderText="Inicia Comunicaci&oacute;n" />
                        <asp:CheckBoxField DataField="AceptaComunicacion" HeaderText="Acepta Comunicaci&oacute;n" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgModificar" runat="server" PostBackUrl='<%# "~/Comunicacion/MantenimientoProtocolo/ModificarProtocolo.aspx?Id="+ Eval("Id") %>'
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
                <asp:ObjectDataSource ID="dsProtocolo" runat="server" DataObjectTypeName="BusinessEntity.Protocolo"
                    DeleteMethod="Eliminar" OldValuesParameterFormatString="original_{0}" SelectMethod="ObtenerTodos"
                    TypeName="BusinessLayer.Comunicacion.ProtocoloBL"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="Button1" runat="server" PostBackUrl="~/Comunicacion/MantenimientoProtocolo/AgregarProtocolo.aspx"
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
</asp:Content>
