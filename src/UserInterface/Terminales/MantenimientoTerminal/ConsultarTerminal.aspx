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
                    DataSourceID="dsEntidadComunicacion" DataTextField="EDC_NOMBRE" 
                    DataValueField="EDC_CODIGO">
                    <asp:ListItem Value="0">Todos</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="dsEntidadComunicacion" runat="server" 
                    OldValuesParameterFormatString="original_{0}" 
                    SelectMethod="obtenerEntidadComunicacion" 
                    TypeName="BusinessLayer.Comunicacion.EntidadComunicacionBL">
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <span class="texto">Estado de terminal </span>
            </td>
            <td>
                <asp:DropDownList ID="ddlEstadoTerminal" runat="server" DataSourceID="oEstadoTerminal"
                    DataTextField="EST_NOMBRE" DataValueField="EST_CODIGO" AppendDataBoundItems="True">
                    <asp:ListItem Value="0">Todos</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="oEstadoTerminal" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="obtenerEstadoTerminal" TypeName="BusinessLayer.Terminales.EstadoTerminalBL">
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
                    DataKeyNames="TRM_CODIGO">
                    <Columns>
                        <asp:BoundField DataField="TRM_SERIAL" HeaderText="Nro Serie" SortExpression="TRM_SERIAL" />
                        <asp:TemplateField HeaderText="Entidad Comunicación">
                            <ItemTemplate>
                                <asp:Label ID="lblEntidadComunicacion" runat="server" Text='<%# Bind("ENTIDAD_COMUNICACION.EDC_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado Terminal">
                            <ItemTemplate>
                                <asp:Label ID="lblEstadoTerminal" runat="server" Text='<%# Bind("ESTADO_TERMINAL.EST_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Punto Servicio">
                            <ItemTemplate>
                                <asp:Label ID="lblPuntoServicio" runat="server" Text='<%# Bind("PUNTO_SERVICIO.PSR_NOMBRE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnModificar" runat="server" PostBackUrl='<%# "~/Terminales/MantenimientoTerminal/ModificarTerminal.aspx?Codigo="+ Eval("TRM_CODIGO") %>'
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
                    SelectMethod="obtenerTerminal" 
                    TypeName="BusinessLayer.Terminales.TerminalBL" 
                    DataObjectTypeName="BusinessEntity.TERMINAL" DeleteMethod="eliminarTerminal">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSerial" DefaultValue="%" Name="serial" PropertyName="Text"
                            Type="String" />
                        <asp:ControlParameter ControlID="ddlEntidadComunicacion" DefaultValue="0" Name="entidadComunicacion"
                            PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter ControlID="ddlEstadoTerminal" DefaultValue="0" Name="estadoTerminal"
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
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
