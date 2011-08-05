<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Principal.Master" AutoEventWireup="true"
    CodeBehind="ModificarPuntoServicio.aspx.cs" Inherits="UserInterface.Terminales.MantenimientoPuntoServicio.ModificarPuntoServicio" %>

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
                <span class="titulo">Modificar Punto de Servicio</span>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:FormView ID="FormView1" runat="server" DataSourceID="dsPuntoServicio" DefaultMode="Edit"
                    DataKeyNames="PSR_CODIGO" HorizontalAlign="Center">
                    <EditItemTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <span class="texto">Nombre</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("Nombre") %>' MaxLength="50"
                                        Width="250px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre"
                                        ErrorMessage="Ingrese el Nombre" ValidationGroup="ModificarPuntoServicio">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="texto"></span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt" runat="server" Text='<%# Bind("") %>'
                                        MaxLength="100" Width="350px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt"
                                        ErrorMessage="Ingrese la " ValidationGroup="ModificarPuntoServicio">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="texto">Estado</span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkEstado" runat="server" Checked='<%# Bind("Estado") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="Button1" runat="server" CommandName="Update" Text="Modificar" ValidationGroup="ModificarPuntoServicio" />
                                    <asp:Button ID="Button2" runat="server" PostBackUrl="~/Terminales/MantenimientoPuntoServicio/ConsultarPuntoServicio.aspx"
                                        Text="Cancelar" />
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="dsPuntoServicio" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="ObtenerPuntoServicio" TypeName="BusinessLayer.Terminales.PuntoServicioBL"
                    DataObjectTypeName="BusinessEntity.PuntoServicio" UpdateMethod="ModificarPuntoServicio"
                    OnUpdated="dsPuntoServicio_Updated">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="codigo" QueryStringField="Codigo" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
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
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="ModificarPuntoServicio" />
</asp:Content>
