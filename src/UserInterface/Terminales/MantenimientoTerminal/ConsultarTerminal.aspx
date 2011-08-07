<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ConsultarTerminal.aspx.cs" Inherits="UserInterface.Terminales.MantenimientoTerminal.ConsultarTerminal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 152px;
        }
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
                <span class="titulo">Consultar Terminal </span>
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
            <td class="style1">
                <span class="texto">Serial </span>
            </td>
            <td>
                <asp:TextBox ID="txtSerial" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <span class="texto">Entidad Comunicación </span>
            </td>
            <td>
                <asp:DropDownList ID="ddlEntidadComunicacion" runat="server" AppendDataBoundItems="True"
                    DataSourceID="dsEntidadComunicacion" DataTextField="Nombre" 
                    DataValueField="Id">
                    <asp:ListItem Value="0">Todos</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="dsEntidadComunicacion" runat="server" 
                    OldValuesParameterFormatString="original_{0}" 
                    SelectMethod="ObtenerTodos" 
                    TypeName="BusinessLayer.Comunicacion.EntidadComunicacionBL">
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <span class="texto">Estado de Terminal </span>
            </td>
            <td>
                <asp:DropDownList ID="ddlEstadoTerminal" runat="server" DataSourceID="oEstadoTerminal"
                    DataTextField="Nombre" DataValueField="Id" AppendDataBoundItems="True">
                    <asp:ListItem Value="0">Todos</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="oEstadoTerminal" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="ObtenerTodos" TypeName="BusinessLayer.Terminales.EstadoTerminalBL">
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
            </td>
        </tr>
        <tr>
            <td class="style1">
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
                <asp:GridView ID="grvListaTerminales" runat="server" AutoGenerateColumns="False"
                    DataSourceID="oTerminal" HorizontalAlign="Center" Width="700px" 
                    DataKeyNames="Id">
                    <Columns>
                        <asp:BoundField DataField="Serial" HeaderText="Nro Serie" SortExpression="Serial" />
                        <asp:TemplateField HeaderText="Entidad Comunicación">
                            <ItemTemplate>
                                <asp:Label ID="lblEntidadComunicacion" runat="server" Text='<%# Bind("EntidadComunicacion.Nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado Terminal">
                            <ItemTemplate>
                                <asp:Label ID="lblEstadoTerminal" runat="server" Text='<%# Bind("EstadoTerminal.Nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Punto Servicio">
                            <ItemTemplate>
                                <asp:Label ID="lblPuntoServicio" runat="server" Text='<%# Bind("PuntoServicio.Nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnModificar" runat="server" PostBackUrl='<%# "~/Terminales/MantenimientoTerminal/ModificarTerminal.aspx?Id="+ Eval("Id") %>'
                                    AlternateText="Modificar" ImageUrl="~/Includes/Imagenes/iconEdit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEliminar" runat="server" CommandName="Delete" AlternateText="Eliminar"
                                    ImageUrl="~/Includes/Imagenes/iconErase.png" OnClientClick="return confirm('Esta seguro que quiere eliminar el Terminal?')"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="oTerminal" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="Buscar" 
                    TypeName="BusinessLayer.Terminales.TerminalBL" 
                    DataObjectTypeName="BusinessEntity.Terminal" 
                    DeleteMethod="Eliminar">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSerial" Name="serial" PropertyName="Text"
                            Type="String" />
                        <asp:ControlParameter ControlID="ddlEntidadComunicacion" DefaultValue="0" Name="entidadComunicacionId"
                            PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter ControlID="ddlEstadoTerminal" DefaultValue="0" Name="estadoTerminalId"
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
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" PostBackUrl="~/Terminales/MantenimientoTerminal/AgregarTerminal.aspx" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
