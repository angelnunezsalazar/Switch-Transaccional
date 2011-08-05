<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="AgregarPuntoServicio.aspx.cs" Inherits="UserInterface.Terminales.MantenimientoPuntoServicio.AgregarPuntoServicio" %>

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
            <td class="style2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                <span class="titulo">Agregar Punto de Servicio</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:FormView ID="FormView1" runat="server" DataSourceID="oPuntoServicio" DefaultMode="Insert" HorizontalAlign="Center">
                    <InsertItemTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <span class="texto">Nombre</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("Nombre") %>' MaxLength="50"
                                        Width="250px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre"
                                        ErrorMessage="Debe ingresar el Nombre" ValidationGroup="AgregarPuntoServicio">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="texto">Dirección</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt" runat="server" Text='<%# Bind("") %>'
                                        MaxLength="100" Width="350px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt"
                                        ErrorMessage="Debe ingresar la Dirección" ValidationGroup="AgregarPuntoServicio">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="texto">Habilitado </span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkHabilitado" runat="server" Checked='<%# Bind("Estado") %>' />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: right">
                                    <asp:Button ID="Button1" runat="server" CommandName="Insert" Text="Aceptar" ValidationGroup="AgregarPuntoServicio" />
                                </td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="Cancelar" PostBackUrl="~/Terminales/MantenimientoPuntoServicio/ConsultarPuntoServicio.aspx" />
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="oPuntoServicio" runat="server" DataObjectTypeName="BusinessEntity.PuntoServicio"
                    InsertMethod="insertarPuntoServicio" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="ObtenerPuntoServicio" TypeName="BusinessLayer.Terminales.PuntoServicioBL"
                    OnInserted="oPuntoServicio_Inserted"></asp:ObjectDataSource>
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
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Se han producido los siguientes errores:"
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="AgregarPuntoServicio" />
</asp:Content>
