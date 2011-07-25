<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="AgregarDetalleReglasValidacion.aspx.cs" Inherits="UserInterface.Mensajeria.MantenimientoReglasValidacion.AgregarDetalleReglasValidacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:FormView ID="fvGrupoValidacion" runat="server" DefaultMode="Edit" DataKeyNames="GRV_CODIGO"
        DataSourceID="dsGrupoValidacion">
        <EditItemTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        C&oacute;digo
                    </td>
                    <td>
                        <asp:Label ID="Codigo" runat="server" Text='<%# Bind("GRV_CODIGO") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Nombre
                    </td>
                    <td>
                        <asp:Label ID="Nombre" runat="server" Text='<%# Bind("GRV_NOMBRE") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Grupo de Mensajes
                    </td>
                    <td>
                        <asp:Label ID="lblGrupoMensaje" runat="server" Text='<%# Bind("MENSAJE.GRUPO_MENSAJE.GMJ_NOMBRE") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Mensaje
                    </td>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server" Text='<%# Bind("MENSAJE.MEN_NOMBRE") %>' />
                        <asp:HiddenField ID="hdnCodigoMensaje" runat="server" Value='<%# Bind("MENSAJE.MEN_CODIGO") %>' />
                    </td>
                </tr>
            </table>
            </div>
        </EditItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="dsGrupoValidacion" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="obtenerGrupoValidacion" TypeName="BusinessLayer.Mensajeria.GrupoValidacionBL">
        <SelectParameters>
            <asp:QueryStringParameter Name="mensaje" QueryStringField="Mensaje" Type="Int32"
                DefaultValue="0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:FormView ID="frvReglaValidacion" runat="server" DataSourceID="dsReglaValidacion"
        DefaultMode="Edit">
        <EditItemTemplate>
            Campo:
            <asp:Label ID="lblCampo" runat="server" Text='<%# Bind("CAMPO.CAM_NOMBRE") %>' />
            <br />
            Criterio de Aplicación:
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Value="0">Seleccione</asp:ListItem>
                <asp:ListItem Value="1">Inclusión</asp:ListItem>
                <asp:ListItem Value="2">Exclusión</asp:ListItem>
                <asp:ListItem Value="3">Especial</asp:ListItem>
            </asp:DropDownList>
            <br />
            Tipo de Regla:&nbsp;<asp:DropDownList ID="DropDownList2" runat="server">
                <asp:ListItem Value="0">Seleccione</asp:ListItem>
                <asp:ListItem Value="1">&lt;</asp:ListItem>
                <asp:ListItem Value="2">&lt;=</asp:ListItem>
                <asp:ListItem Value="3">=</asp:ListItem>
                <asp:ListItem Value="4">&lt;&gt;</asp:ListItem>
                <asp:ListItem Value="5">&gt;=</asp:ListItem>
                <asp:ListItem Value="6">&gt;</asp:ListItem>
            </asp:DropDownList>
            <br />
            Valor de Comparaci&oacute;n:
            <asp:TextBox ID="txtValorComparacion" runat="server"></asp:TextBox>
            <br />
            &nbsp;<asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False"
                CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="dsReglaValidacion" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="obtenerReglaValidacion" TypeName="BusinessLayer.Mensajeria.ReglaValidacionBL">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="grupoValidacion" QueryStringField="Grupo"
                Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="campo" QueryStringField="Campo"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
