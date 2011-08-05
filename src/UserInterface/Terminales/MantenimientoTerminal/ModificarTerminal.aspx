<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ModificarTerminal.aspx.cs" Inherits="UserInterface.Terminales.MantenimientoTerminal.ModificarTerminal" %>

<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 19px;
            text-align: center;
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
                <span class="titulo">Modificar Terminal</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:FormView ID="fvTerminal" runat="server" DataSourceID="oTerminal" DefaultMode="Edit"
                    HorizontalAlign="Center" Width="400px">
                    <EditItemTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td class="style2">
                                    <span class="texto">Número de Serie</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSerial" runat="server" MaxLength="20" Width="160px" Text='<%# Bind("TRM_SERIAL") %>' />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSerial"
                                        ErrorMessage="Debe ingresar el Número de Serie" ValidationGroup="AgregarTerminal">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <span class="texto">Entidad de Comunicación</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEntidad" runat="server" DataSourceID="oEntidad" DataTextField="EDC_NOMBRE"
                                        DataValueField="EDC_CODIGO" AppendDataBoundItems="True" Width="200px"
                                        SelectedValue='<%# Eval("EntidadComunicacion.EDC_CODIGO")%>'>
                                        <asp:ListItem Value="-1">Seleccionar Entidad</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlEntidad"
                                        ErrorMessage="Debe ingresar la Entidad de Comunicación" InitialValue="-1" ValidationGroup="AgregarTerminal">*</asp:RequiredFieldValidator>
                                    <asp:ObjectDataSource ID="oEntidad" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="obtenerEntidadComunicacion" TypeName="BusinessLayer.Comunicacion.EntidadComunicacionBL">
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <span class="texto">Punto de Servicio</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPtoServicio" runat="server" DataSourceID="oPtoServicio"
                                        DataTextField="PSR_NOMBRE" DataValueField="PSR_CODIGO" AppendDataBoundItems="True"
                                        Width="200px" SelectedValue='<%# Eval("PuntoServicio.PSR_CODIGO")%>'>
                                        <asp:ListItem Value="-1">Seleccionar Punto de Servicio</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPtoServicio"
                                        ErrorMessage="Debe ingresar el Punto de Servicio" InitialValue="-1" ValidationGroup="AgregarTerminal">*</asp:RequiredFieldValidator>
                                    <asp:ObjectDataSource ID="oPtoServicio" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="ObtenerPuntoServicio" TypeName="BusinessLayer.Terminales.PuntoServicioBL">
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <span class="texto">Estado del Terminal </span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEstadoTerminal" runat="server" DataSourceID="dsEstadoTerminal"
                                        DataTextField="EST_NOMBRE" DataValueField="EST_CODIGO" AppendDataBoundItems="True"
                                        Width="200px" SelectedValue='<%# Eval("EstadoTerminal.EST_CODIGO")%>'>
                                        <asp:ListItem Value="-1">Seleccionar Estado Terminal</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlEstadoTerminal"
                                        ErrorMessage="Debe ingresar el Estado del Terminal" InitialValue="-1" ValidationGroup="AgregarTerminal">*</asp:RequiredFieldValidator>
                                    <asp:EntityDataSource ID="dsEstadoTerminal" runat="server" ConnectionString="name=Switch"
                                        DefaultContainerName="Switch" EntitySetName="EstadoTerminal">
                                    </asp:EntityDataSource>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Button ID="btnAceptar" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="AgregarTerminal" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" PostBackUrl="~/Terminales/MantenimientoTerminal/ConsultarTerminal.aspx" />
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="oTerminal" runat="server" DataObjectTypeName="BusinessEntity.Terminal"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="obtenerTerminal"
                    TypeName="BusinessLayer.Terminales.TerminalBL" UpdateMethod="modificarTerminal"
                    OnUpdating="oTerminal_Updating" onupdated="oTerminal_Updated">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="Codigo" QueryStringField="Codigo" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Se han producido los siguientes errores:"
                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="AgregarTerminal" />
            </td>
        </tr>
    </table>
</asp:Content>
